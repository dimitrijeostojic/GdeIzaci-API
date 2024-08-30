using GdeIzaci.Models.Domain;

namespace GdeIzaci.Models.DTO
{
    public class PlaceDTO
    {
        public Guid PlaceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public string Photo { get; set; }
        public Guid UserCreatedID { get; set; }
        public Guid PlaceItemID { get; set; }

        //navigation properties
        public PlaceItemDTO PlaceItem { get; set; }
        public ICollection<ReviewDTO> Reviews { get; set; }
        //public ICollection<ReservationDTO> Reservations { get; set; }
    }
}
