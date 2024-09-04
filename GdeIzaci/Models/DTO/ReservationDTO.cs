namespace GdeIzaci.Models.DTO
{
    public class ReservationDTO
    {
        public Guid Id { get; set; }
        public int NumberOfUsers { get; set; }
        public Guid UserId { get; set; }
        public Guid PlaceID { get; set; }
        public DateTime ReservationDateTime { get; set; }
    }
}
