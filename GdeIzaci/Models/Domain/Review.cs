using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.Domain
{
    public class Review
    {
        [Key]
        public Guid ReviewID { get; set; }
        public string TypeOfReview { get; set; }
        public string CommentOfReview { get; set; }
        public string NumberOfStars { get; set; }
        public Guid UserID { get; set; }
        public Guid PlaceID { get; set; }


        //Navigation properties
        public User User { get; set; }
        public Place Place { get; set; }
    }
}
