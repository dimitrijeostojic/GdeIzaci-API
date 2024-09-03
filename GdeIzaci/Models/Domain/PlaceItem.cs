using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.Domain
{
    public class PlaceItem
    {
        [Key]
        public Guid PlaceItemID { get; set; }
        public string Name { get; set; }

        //Navigation property

        public ICollection<Place> Places { get; } = new List<Place>();
    }
}
