using Garage_2._0.Common;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models.Entities
{
    public class Vehicle
    {
        [Required]
        public string RegNo { get; set; }

        public int Id { get; set; }

        [Range(1, 14)]
        public int Wheels { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public VehicleTypes VehicleType { get; set; }


        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }
    }
}
