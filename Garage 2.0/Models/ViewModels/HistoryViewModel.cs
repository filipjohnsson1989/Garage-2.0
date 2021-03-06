using Garage_2._0.Common;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models.ViewModels
{
    public class HistoryViewModel
    {
        [Required]
        [Display(Name = "Reg num")]
        public string RegNo { get; set; }

        public int Id { get; set; }

        [Range(0, 14)]
        [Display(Name = "Antal hjul")]
        public int Wheels { get; set; }

        [Display(Name = "Märke")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Display(Name = "Färg")]
        public string? Color { get; set; }

        [Display(Name = "Typ")]
        public VehicleTypes VehicleType { get; set; }

        [Display(Name = "Incheckningstid")]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Utcheckningstid")]
        public DateTime? CheckOut { get; set; }

        [Display(Name = "Parkeringskostnad")]
        public double? ParkingCost { get; set; }

        [Display(Name = "Parkeringskostnad")]
        public string DisplayParkingCost => $" {ParkingCost:C2}";
    }
}
