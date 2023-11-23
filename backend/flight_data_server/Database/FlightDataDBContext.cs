﻿using flight_data_server.Models.Airliner;
using flight_data_server.Models.FlightData;
using Microsoft.EntityFrameworkCore;

namespace flight_data_server.Database
    {
    public class FlightDataDBContext : DbContext
        {
        public FlightDataDBContext(DbContextOptions<FlightDataDBContext> options) : base(options)
            {

            }

        public DbSet<FlightData> FlightData { get; set; }
        public DbSet<FlightInfo> FlightInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
            {

            modelbuilder.Entity<FlightData>();
            modelbuilder.Entity<FlightInfo>();
            }

        internal void AddAsync() => throw new NotImplementedException();

        }
    }
