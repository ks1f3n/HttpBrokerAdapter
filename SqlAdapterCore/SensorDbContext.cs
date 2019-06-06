using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SqlAdapterCore.Model;

namespace SqlAdapterCore
{
    public class SensorDbContext : DbContext
    {
        public SensorDbContext(DbContextOptions<SensorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Gap> Gap { get; set; }
        public DbSet<Incl> Incl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sensor.db");
        }
    }
    //https://docs.microsoft.com/ru-ru/ef/core/get-started/aspnetcore/new-db?tabs=visual-studio
}
