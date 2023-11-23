using flight_data_server.Models.Airliner;
using flight_data_server.Models.FlightData;
using flight_data_server.Models.User;
using Microsoft.EntityFrameworkCore;

namespace flight_data_server.Database
    {
    public class UserDBContext : DbContext
        {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
            {

            }

        public DbSet<User> UserData { get; set; }
        public DbSet<PilotProfileModel> PilotProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
            {

            modelbuilder.Entity<User>();
            modelbuilder.Entity<PilotProfileModel>();
            }

        internal void AddAsync() => throw new NotImplementedException();

        }
    }
