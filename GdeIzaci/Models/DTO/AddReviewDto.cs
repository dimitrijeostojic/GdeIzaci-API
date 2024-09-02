using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.DTO
{
    public class AddReviewDto
    {
        [Required]
        public double NumberOfStars { get; set; }
        [Required]
        public Guid PlaceID { get; set; }
        [Required]
        public Guid UserID { get; set; }
    }
}
