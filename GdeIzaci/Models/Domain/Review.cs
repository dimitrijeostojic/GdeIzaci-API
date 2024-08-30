using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.Domain
{
    public class Review
    {
        [Key]
        public Guid ReviewID { get; set; }
        public int numberOfStars { get; set; }
        public Guid UserID { get; set; }
        public Guid PlaceID { get; set; }

    }
}
