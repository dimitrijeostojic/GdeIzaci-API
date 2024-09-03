using GdeIzaci.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GdeIzaci.Data
{
    public class GdeIzaciDBContext : DbContext
    {

        public GdeIzaciDBContext(DbContextOptions<GdeIzaciDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<PlaceItem> PlaceItems { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed placeItems to the databse
            modelBuilder.Entity<PlaceItem>().HasData(SeedPlaceItems());
        }
        public List<PlaceItem> SeedPlaceItems()
        {
            var placeItems = new List<PlaceItem>();
            using (StreamReader r = new StreamReader(@"C:\Users\dimit\Desktop\gde izaci .net\GdeIzaci\GdeIzaci\Files\PlaceItem.json"))
            {
                string json = r.ReadToEnd();
                placeItems = JsonConvert.DeserializeObject<List<PlaceItem>>(json);
            }
            return placeItems;
        }
    }
}
