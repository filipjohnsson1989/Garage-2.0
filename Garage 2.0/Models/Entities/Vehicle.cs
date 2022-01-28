using Garage_2._0.Common;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public VehicleType VehicleType { get; set; }

    public string RegNo { get; set; }
    public string Color { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
}
