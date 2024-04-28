namespace GdeIzaci.Models.DTO
{
    public class AddPlaceRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int NumberOfReviews { get; set; }
        public Guid UserCreatedID { get; set; }
        public Guid UserReservedID { get; set; }
        public Guid PlaceItemID { get; set; }
    }
}
