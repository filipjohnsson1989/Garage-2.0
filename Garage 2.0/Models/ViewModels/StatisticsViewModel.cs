using Garage_2._0.Common;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models.ViewModels
{
    public class StatisticsViewModel
    {
        private readonly double _parkingHourlyCost;
        public StatisticsViewModel(double parkingHourlyCost)
        {
            _parkingHourlyCost = parkingHourlyCost;
        }
        
        [Display(Name = "Fordonstyp")]
        public VehicleTypes VehicleType { get; set; }
        [Display(Name = "Antal fordon")]
        public int NumOfVehicles { get; set; }
        
        [Display(Name = "Antal hjul")]
        public int NumOfWheels { get; set; }

        [Display(Name = "Totalt antal minuter")]
        public int TotalTime { get;set; }

        public string ParkingTime { get { return Util.ParkingTimeString(TimeSpan.FromMinutes(TotalTime)); } }

        public int TotalUnpaidTime { get; set; }
        public double TotalParkingCost { get; set; }
        public double Payment { get { return this.TotalParkingCost + Util.ParkingTimeCost(this.TotalUnpaidTime, _parkingHourlyCost); }  }

        [Display(Name = "Summa SEK")]
        public string PaymentText { get { return String.Format(" {0:C2}", this.Payment); }  }
    }
}
