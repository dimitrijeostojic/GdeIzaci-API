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
        public string Location { get; set; }
        public int Price { get; set; }
        public string Photo { get; set; }
        public Guid UserCreatedID { get; set; }
        public Guid PlaceItemID { get; set; }

        //navigation properties
        public PlaceItem PlaceItem { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
