using GdeIzaci.Models.Domain;

namespace GdeIzaci.Models.DTO
{
    public class UserDTO
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //this can be enum or asp.net roles with jwt
        

        //Navigation property
        public ICollection<ReviewDTO> Reviews { get; } = new List<ReviewDTO>();
        public ICollection<PlaceDTO> CreatedPlaces { get; } = new List<PlaceDTO>();
        public ICollection<PlaceDTO> ReservedPlaces { get; } = new List<PlaceDTO>();
    }
}
