using System.Configuration;

namespace Garage_2._0.Common
{
    public class Util
    {
        //public static double _hourlyParkingCost { get; private set; }

        //public Util(IConfiguration config)
        //{
        //    var timeRate = config.GetSection("Garage");

        //    //ToDo ParkingTimeCost: Get TimeRate from Config(appsettings)
        //    if (double.TryParse(config["Garage:HourlyCarge"], out double hourlyTimeRate))
        //        _hourlyParkingCost = hourlyTimeRate;
        //   else
        //        _hourlyParkingCost = 0;
        //}

        /// <summary>
        /// Calculates the time (in minutes) that a vehicle has been parked. 
        /// </summary>
        /// <param name="checkIn">Time for check in</param>
        /// <param name="checkOut">Time for check out</param>
        /// <returns>Returns the parked time in minutes</returns>
        public static double ParkingTimeMin(DateTime checkIn, DateTime checkOut)
        {
            if (checkIn > checkOut)
                throw new ArgumentException("Can not check out before checking in.");

            TimeSpan timeSpan = (checkOut - checkIn);
            return timeSpan.TotalMinutes;
        }

        /// <summary>
        /// Creates a string representing the time a vehicle has been parked
        /// </summary>
        /// <param name="checkIn">Time for check in</param>
        /// <param name="checkOut">Time for check out</param>
        /// <returns>Returns string representantion of the parked time.
        /// <example>45 m, 1 d 1 h 45 m</example></returns>
        public static string ParkingTimeString(DateTime checkIn, DateTime checkOut)
        {
            TimeSpan timeSpan = checkOut - checkIn;
            return ParkingTimeString(timeSpan);
        }

        /// <summary>
        /// Creates a string representing the time a vehicle has been parked
        /// </summary>
        /// <param name="datesDiff">Differentiate of check in and checkout</param>
        /// <returns>Returns string representantion of the parked time.
        /// <example>45 m, 1 d 1 h 45 m</example></returns>
        public static string ParkingTimeString(TimeSpan datesDiff)
        {
            string timeString = "";

            if (datesDiff.Days != 0) timeString += datesDiff.Days.ToString() + " d ";
            if (datesDiff.Hours != 0) timeString += datesDiff.Hours.ToString() + " t ";
            if (datesDiff.Minutes != 0) timeString += datesDiff.Minutes.ToString() + " m ";
            //return timeSpan.Days + " " + timeSpan.Hours + ":" + timeSpan.Minutes + " " + String.Format(" {0:C2}", (timeSpan.TotalMinutes * 10 / 60));
            return timeString;
        }


        /// <summary>
        /// Calculates the cost of a parked vehicle.
        /// </summary>
        /// <param name="checkIn">Time for check in</param>
        /// <param name="checkOut">Time for check out</param>
        /// <param name="hourlyCost">The hourly cost</param>
        /// <returns>Returns the calculated cost rounded to two decimal</returns>
        public static double ParkingTimeCost(DateTime checkIn, DateTime checkOut, double hourlyCost)
        {
            TimeSpan timeSpan = checkOut - checkIn;

            //Round down to full minutes
            return ParkingTimeCost(timeSpan, hourlyCost);
            
        }

        /// <summary>
        /// Calculates the cost of a parked vehicle.
        /// </summary>
        /// <param name="datesDiff">Differentiate of check in and checkout</param>
        /// <param name="hourlyCost">The hourly cost</param>
        /// <returns>Returns the calculated cost rounded to two decimal</returns>
        public static double ParkingTimeCost(TimeSpan datesDiff, double hourlyCost)
        {
            //Round down to full minutes
            return ParkingTimeCost(datesDiff.TotalMinutes, hourlyCost);

        }

        /// <summary>
        /// Calculates the cost of a parked vehicle.
        /// </summary>
        /// <param name="TotalMinutes">Differentiate of check in and checkout in number of minutes</param>
        /// <param name="hourlyCost">The hourly cost</param>
        /// <returns>Returns the calculated cost rounded to two decimal</returns>
        public static double ParkingTimeCost(double TotalMinutes, double hourlyCost)
        {
            //Round down to full minutes
            return Math.Round(Math.Round(TotalMinutes, 0, MidpointRounding.ToZero) * hourlyCost / 60.0, 2);

        }
    }
}
