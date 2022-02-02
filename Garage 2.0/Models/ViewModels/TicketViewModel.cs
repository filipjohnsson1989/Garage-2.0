﻿using Garage_2._0.Common;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models.ViewModels
{
    public class TicketViewModel
    {
        [Display(Name = "Reg nr")]
        [Required]
        public string RegNo { get; set; }

        public int Id { get; set; }

        [Display(Name = "Antal hjul")]
        [Range(0, 14)]
        public int Wheels { get; set; }

        [Display(Name = "Märke")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Display(Name = "Fordonstyp")]
        public VehicleTypes VehicleType { get; set; }

        [Display(Name = "Check in tid")]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check out tid")]
        public DateTime CheckOut { get; set; }

        [Display(Name = "Parkerad tid")]
        public string ParkingTime 
        { 
            get
            {
                return Util.ParkingTimeString(CheckIn, DateTime.Now);
            } 
        }

        [Display(Name = "Kostnad")]
        public string Price
        {
            get
            {
                return String.Format(" {0:C2}", Util.ParkingTimeCost(CheckIn, DateTime.Now, HourlyCost));
            }
        }

        [Display(Name = "Timkostnad")]
        public double HourlyCost { get; internal set; }
    }
}