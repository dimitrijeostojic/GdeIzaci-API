using GdeIzaci.Models.Domain;

namespace GdeIzaci.Models.DTO
{
    public class PlaceDTO
    {
        public Guid PlaceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int NumberOfReviews { get; set; }
      

        //Navigation properties
        public User PlaceReservedBy { get; set; }
        public User PlaceCreatedBy { get; set; }
        public PlaceItemDTO PlaceItem { get; set; }
        public ICollection<ReviewDTO> Review { get; set; }
    }
}
