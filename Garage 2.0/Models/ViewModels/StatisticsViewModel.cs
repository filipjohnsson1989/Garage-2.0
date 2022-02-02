using Garage_2._0.Common;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models.ViewModels
{
    public class StatisticsViewModel
    {
        [Display(Name = "Fordonstyp")]
        public VehicleTypes VehicleType { get; set; }
        [Display(Name = "Antal fordon")]
        public int NumOfVehicles { get; set; }
        
        [Display(Name = "Antal hjul")]
        public int NumOfWheels { get; set; }

        [Display(Name = "Totalt antal minuter")]
        public int TotalTime { get;set; }

        [Display(Name = "Summa SEK")]
        public string Payment { get; set; }

    }
}
