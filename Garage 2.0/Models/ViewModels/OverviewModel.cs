using Garage_2._0.Common;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models.ViewModels
{
    public class OverviewModel
    {
        public int Id { get; set; }

        [Display(Name = "Typ")]
        public VehicleTypes VehicleType { get; set; }

        [Required]
        [Display(Name = "Reg num")]
        public string RegNo { get; set; }


        [Display(Name = "Incheckningstid")]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Parkerad tid")]
        public string ParkingTime
        {
            get
            {
                return Util.ParkingTimeString(CheckIn, DateTime.Now);
            }
        }

        [Display(Name = "Aktuell kostnad")]
        public string Price
        {
            get
            {
                return String.Format(" {0:C2}", Util.ParkingTimeCost(CheckIn, DateTime.Now, 10.0));
            }
        }
    }
}
