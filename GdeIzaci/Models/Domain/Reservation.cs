using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.Domain
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        public int NumberOfUsers { get; set; }
        public Guid UserId { get; set; }
        public Guid PlaceID { get; set; }
        public DateTime ReservationDateTime { get; set; }

        //navigation properties
        public Place Place { get; set; }
    }
}
