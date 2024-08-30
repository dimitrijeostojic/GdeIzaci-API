using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.DTO
{
    public class AddPlaceRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name has to be minimum of 3 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Description has to be maximum of 200 characters")]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [Range(0,100000)]
        public int Price { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public Guid PlaceItemID { get; set; }
    }
}
