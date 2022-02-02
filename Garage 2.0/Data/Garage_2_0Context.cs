#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage_2._0.Models.Entities;

namespace Garage_2._0.Data
{
    public class Garage_2_0Context : DbContext
    {
        public Garage_2_0Context(DbContextOptions<Garage_2_0Context> options)
            : base(options)
        {
        }

        public DbSet<Models.Entities.Vehicle> Vehicle { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehicle>()
                .HasData(
                new Vehicle { Id = 1, VehicleType = Common.VehicleTypes.Car, RegNo = "ABC123", Brand = "Volvo", Model = "V70", Wheels = 4, CheckIn = DateTime.Now.AddHours(-1) },
                new Vehicle { Id = 2, VehicleType = Common.VehicleTypes.Car, RegNo = "ABC456", Brand = "Messerschitt", Model = "KR 200", Wheels = 3, CheckIn = DateTime.Now.AddMinutes(-15) },
                new Vehicle { Id = 3, VehicleType = Common.VehicleTypes.MC, RegNo = "ABC789", Brand = "Honda", Model = "CB 125T", Wheels = 2, CheckIn = DateTime.Now.AddMinutes(-30) },
                new Vehicle { Id = 4, VehicleType = Common.VehicleTypes.Bus, RegNo = "DEF123", Brand = "MAN", Model = "X-2000", Wheels = 6, CheckIn = DateTime.Now.AddMinutes(-5) },
                new Vehicle { Id = 5, VehicleType = Common.VehicleTypes.Boat, RegNo = "DEF456", Brand = "Nautor Swan", Model = "Swan 66", Wheels = 0, CheckIn = DateTime.Now.AddDays(-30) }
                );
        }
    }
}
