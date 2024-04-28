using GdeIzaci.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.Domain
{
    public class Place
    {
        [Key]
        public Guid PlaceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int NumberOfReviews { get; set; }
        public Guid UserCreatedID { get; set; }
        public Guid UserReservedID { get; set; }
        public Guid PlaceItemID { get; set; }

        //Navigation properties
        public PlaceItem PlaceItem { get; set; }
        public User PlaceReservedBy { get; set; }
        public User PlaceCreatedBy { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
