using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.Domain
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       

        //Navigation property
        public ICollection<Review> Reviews { get; } = new List<Review>();
        public ICollection<Place> CreatedPlaces { get; } = new List<Place>();
        public ICollection<Place> ReservedPlaces { get; } = new List<Place>();


    }
}
