using GdeIzaci.Models.Domain;

namespace GdeIzaci.Models.DTO
{
    public class ReviewDTO
    {
        public Guid ReviewID { get; set; }
        public double numberOfStars { get; set; }
        public Guid UserID { get; set; }
        public Guid PlaceID { get; set; }
    }
}
