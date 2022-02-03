using Garage_2._0.Common;

namespace Garage_2._0.Models.ViewModels
{
    public class VehicleIndexViewModel
    {
        public IEnumerable<Garage_2._0.Models.ViewModels.ParkingDetailModel> Vehicles { get; set; }

        public GarageSize GarageSize { get; set; } 
    }
}
