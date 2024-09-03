using AutoMapper;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GdeIzaci.Services.Implementations
{
    public class ReviewService : IReviewService
    {

        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<ReviewDTO> CreateReviewAsync(AddReviewDto addReviewDto, Guid userId)
        {
            var review = mapper.Map<Review>(addReviewDto);
            review.ReviewID = Guid.NewGuid();
            review.UserID = userId;

            review = await reviewRepository.CreateAsync(review);

            return mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO?> DeleteReviewAsync(Guid id)
        {
            var review = await reviewRepository.DeleteAsync(id);
            return review != null ? mapper.Map<ReviewDTO>(review) : null;
        }

        public async Task<List<ReviewDTO>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var reviews = await reviewRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return mapper.Map<List<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO?> GetAverageByIdAsync(Guid placeId)
        {
            var review = await reviewRepository.GetAverageAsync(placeId);
            return mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO?> GetByIdAsync(Guid id)
        {
            var review = await reviewRepository.GetByIdAsync(id);
            return review != null ? mapper.Map<ReviewDTO>(review) : null;
        }

        public async Task<ReviewDTO?> GetByPlaceIdUserIdAsync(Guid placeId, Guid userId)
        {
            var review = await reviewRepository.GetByPlaceIdUserIdAsync(placeId, userId);
            return review != null ? mapper.Map<ReviewDTO>(review) : null;
        }

        public async Task<ReviewDTO?> UpdateReviewAsync(Guid id, UpdateReviewDto updateReviewDto)
        {
            var review = mapper.Map<Review>(updateReviewDto);
            review = await reviewRepository.UpdateAsync(id, review);
            return review != null ? mapper.Map<ReviewDTO>(review) : null;
        }


    }
}
