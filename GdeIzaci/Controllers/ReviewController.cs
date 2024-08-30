using AutoMapper;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
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
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IPlaceRepository placeRepository;
        private readonly UserManager<IdentityUser> userManager;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IPlaceRepository placeRepositorty, UserManager<IdentityUser> userManager)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.placeRepository = placeRepositorty;
            this.userManager = userManager;
        }

        [HttpGet] // https://localhost:5000/api/Review
        [Authorize(Roles = "RegularUser, Manager")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {

            var reviews = await reviewRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            var reviewsDto = mapper.Map<List<ReviewDTO>>(reviews);

            return Ok(reviewsDto);

        }

        [HttpPost]
        [Authorize(Roles = "RegularUser")]
        public async Task<IActionResult> CreateReview([FromBody] AddReviewDto addReviewDto)
        {
            var currentUser = userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var review = mapper.Map<Review>(addReviewDto);
            review.ReviewID = Guid.NewGuid();
            review.UserID = addReviewDto.UserID;

            review = await reviewRepository.CreateAsync(review);

            var reviewDto = mapper.Map<ReviewDTO>(review);


            return CreatedAtAction(nameof(GetReviewById), new { id = review.ReviewID }, reviewDto);

        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetReviewById([FromRoute] Guid id)
        {
            var review = await reviewRepository.GetByIdAsync(id);

            if (review == null)
            {
                return BadRequest("Place doesn't exist");
            }
            //Convert to Dto
            var reviewDTO = mapper.Map<ReviewDTO>(review);
            return Ok(reviewDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteReview([FromRoute] Guid id)
        {
            var review = await reviewRepository.DeleteAsync(id);

            if (review == null)
            {
                return BadRequest("This review doesn't exist");
            }

            var reviewDto = mapper.Map<ReviewDTO>(review);
            return Ok(reviewDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateReview([FromRoute]Guid id, [FromBody] UpdateReviewDto updateReviewDto)
        {

            var review = mapper.Map<Review>(updateReviewDto);

            review = await reviewRepository.UpdateAsync(id, review);

            var reviewDto = mapper.Map<ReviewDTO>(review);

            return NoContent();
        }

    }
}
