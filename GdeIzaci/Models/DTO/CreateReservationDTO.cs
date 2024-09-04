using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.DTO
{
    public class CreateReservationDTO
    {
        [Required]
        public int NumberOfUsers { get; set; }
        [Required]
        public Guid PlaceID { get; set; }
        [Required]
        public Guid UserID { get; set; }
        [Required]
        public DateTime ReservationDateTime { get; set; }
    }
}
