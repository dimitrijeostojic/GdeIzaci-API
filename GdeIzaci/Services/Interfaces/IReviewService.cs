using GdeIzaci.Models.DTO;

namespace GdeIzaci.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDTO>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize);
        Task<ReviewDTO> CreateReviewAsync(AddReviewDto addReviewDto, Guid userId);
        Task<ReviewDTO?> GetAverageByIdAsync(Guid placeId);
        Task<ReviewDTO?> DeleteReviewAsync(Guid id);
        Task<ReviewDTO?> UpdateReviewAsync(Guid id, UpdateReviewDto updateReviewDto);
        Task<ReviewDTO?> GetByPlaceIdUserIdAsync(Guid placeId, Guid userId);
        Task<ReviewDTO?> GetByIdAsync(Guid id);
    }
}
