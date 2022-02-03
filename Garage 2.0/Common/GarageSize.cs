using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Common
{
    public class GarageSize
    {

        public double MaxCapacity = 10;

        [Range(0, 10, ErrorMessage = "Kan bara ett visst antal fordon i garaget.")]
        public double Size = 0;
    }
}