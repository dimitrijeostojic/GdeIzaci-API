using GdeIzaci.Models.Domain;

namespace GdeIzaci.Models.DTO
{
    public class ReviewDTO
    {
        public Guid ReviewID { get; set; }
        public string TypeOfReview { get; set; }
        public string CommentOfReview { get; set; }
        public string NumberOfStars { get; set; }


        //Navigation properties
        public UserDTO User { get; set; }
        public PlaceDTO Place { get; set; }
    }
}
