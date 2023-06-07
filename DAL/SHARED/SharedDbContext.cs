﻿using Microsoft.EntityFrameworkCore;
using BOL.SHARED;

namespace DAL.SHARED
{
    public class SharedDbContext : DbContext
    {
        public SharedDbContext(DbContextOptions<SharedDbContext> options)
            : base(options)
        {
        }

        public DbSet<Designation> Designation { get; set; }
        public DbSet<NatureOfBusiness> NatureOfBusiness { get; set; }
        public DbSet<Turnover> Turnover { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Station> Station { get; set; }
        public DbSet<Pincode> Pincode { get; set; }
        public DbSet<Locality> Locality { get; set; }

        public DbSet<Messages> Messages { get; set; }
    }
}