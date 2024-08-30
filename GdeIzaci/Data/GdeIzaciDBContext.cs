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

            //// Veza Place - PlaceItem (1-M)
            //modelBuilder.Entity<Place>()
            //    .HasMany(p => p.PlaceItem)
            //    .WithOne(pi => pi.Place)
            //    .HasForeignKey(pi => pi.PlaceID);

            //// Veza Place - Reservation (1-M)
            //modelBuilder.Entity<Place>()
            //    .HasMany(p => p.Reservations)
            //    .WithOne(r => r.Place)
            //    .HasForeignKey(r => r.Place);

            //// Veza Place - Review (1-M)
            //modelBuilder.Entity<Place>()
            //    .HasMany(p => p.Reviews)
            //    .WithOne(r => r.Place)
            //    .HasForeignKey(r => r.PlaceID);




            //Seed placeItems to the databse
            modelBuilder.Entity<PlaceItem>().HasData(SeedPlaceItems());
            //modelBuilder.Entity<Place>().HasData(SeedPlaces());
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

        public List<Place> SeedPlaces()
        {
            var places = new List<Place>();
            using(StreamReader r = new StreamReader(""))
            {
                string json = r.ReadToEnd();
                places=JsonConvert.DeserializeObject<List<Place>>(json);
            }
            return places;
        }
    }
}
