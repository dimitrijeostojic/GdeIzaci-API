using AutoMapper;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<IdentityUser> userManager;

        public ReviewController(IReviewService reviewService, UserManager<IdentityUser> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpGet] // https://localhost:5000/api/Review
        [Authorize(Roles = "RegularUser, Manager")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var reviewsDto = await reviewService.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(reviewsDto);
        }

        [HttpPost]
        [Authorize(Roles = "RegularUser, Manager")]
        public async Task<IActionResult> CreateReview([FromBody] AddReviewDto addReviewDto)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewDto = await reviewService.CreateReviewAsync(addReviewDto, Guid.Parse(user.Id));
            return CreatedAtAction(nameof(GetById), new { id = reviewDto.PlaceID }, reviewDto);
        }


        [HttpGet("average-rating/{placeId:Guid}")]
        public async Task<IActionResult> GetAverageById(Guid placeId)
        {
            var averageRating = await reviewService.GetAverageByIdAsync(placeId);
            return averageRating != null ? Ok(averageRating) : NotFound();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteReview([FromRoute] Guid id)
        {
            var reviewDto = await reviewService.DeleteReviewAsync(id);
            return reviewDto != null ? Ok(reviewDto) : BadRequest("This review doesn't exist");

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateReview([FromRoute] Guid id, [FromBody] UpdateReviewDto updateReviewDto)
        {

            var reviewDto = await reviewService.UpdateReviewAsync(id, updateReviewDto);
            return reviewDto != null ? Ok(reviewDto) : BadRequest("Failed to update review");
        }

        [HttpGet]
        [Route("{id:Guid}/{userId:Guid}")]
        public async Task<IActionResult> GetByPlaceIdUserIdAsync(Guid id,Guid userId)
        {
            var reviewDto = await reviewService.GetByPlaceIdUserIdAsync(id, userId);
            return reviewDto != null ? Ok(reviewDto) : BadRequest("Review doesn't exist");
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var reviewDto = await reviewService.GetByIdAsync(id);
            return reviewDto != null ? Ok(reviewDto) : BadRequest("Review doesn't exist");
        }

    }
}
