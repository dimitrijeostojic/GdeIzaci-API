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

        public DbSet<User> Users { get; set; }
        public DbSet<PlaceItem> PlaceItems { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
        .HasMany(u => u.Reviews)
        .WithOne(p => p.User)
        .HasForeignKey(p => p.UserID)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
        .HasMany(u => u.CreatedPlaces)
        .WithOne(p => p.PlaceCreatedBy)
        .HasForeignKey(p => p.UserCreatedID)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
        .HasMany(u => u.ReservedPlaces)
        .WithOne(p => p.PlaceReservedBy)
        .HasForeignKey(p => p.UserReservedID)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Place>()
        .HasMany(u => u.Reviews)
        .WithOne(p => p.Place)
        .HasForeignKey(p => p.PlaceID)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlaceItem>()
        .HasMany(u => u.Places)
        .WithOne(p => p.PlaceItem)
        .HasForeignKey(p => p.PlaceItemID)
        .OnDelete(DeleteBehavior.Restrict);


            //Seed places to the databse
            //modelBuilder.Entity<Place>().HasData(SeedPlaces());
            modelBuilder.Entity<PlaceItem>().HasData(SeedPlaceItems());
            //modelBuilder.Entity<User>().HasData(SeedUsers());
            //modelBuilder.Entity<Review>().HasData(SeedReviews());
        }


        public List<Place> SeedPlaces()
        {
            var places = new List<Place>();
            using (StreamReader r = new StreamReader(@"json file path"))
            {
                string json = r.ReadToEnd();
                places = JsonConvert.DeserializeObject<List<Place>>(json);
            }
            return places;
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
        public List<User> SeedUsers()
        {
            var users = new List<User>();
            using (StreamReader r = new StreamReader(@"json file path"))
            {
                string json = r.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            return users;
        }
        public List<Review> SeedReviews()
        {
            var reviews = new List<Review>();
            using (StreamReader r = new StreamReader(@"json file path"))
            {
                string json = r.ReadToEnd();
                reviews = JsonConvert.DeserializeObject<List<Review>>(json);
            }
            return reviews;
        }
    }
}
