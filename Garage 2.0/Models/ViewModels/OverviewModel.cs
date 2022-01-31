using Garage_2._0.Common;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models.ViewModels
{
    public class OverviewModel
    {
        public int Id { get; set; }

        public VehicleTypes VehicleType { get; set; }

        [Required]
        public string RegNo { get; set; }


        //[Range(1, 14)]
        //public int Wheels { get; set; }

        //public string Brand { get; set; }

        //public string Model { get; set; }


        public DateTime CheckIn { get; set; }

        //public TimeSpan ParkingTime => (DateTime.Now - CheckIn).TotalHours;
        //public double ParkingTime => (DateTime.Now - CheckIn).TotalHours;
        public string ParkingTime
        {
            get
            {
                TimeSpan timeSpan = (DateTime.Now - CheckIn);
                return timeSpan.Days + " " + timeSpan.Hours + ":" + timeSpan.Minutes + " " + String.Format(" {0:C2}",(timeSpan.TotalMinutes * 10 / 60));
            }
        }
    }
}
