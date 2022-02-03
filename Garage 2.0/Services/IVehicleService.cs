using Garage_2._0.Common;
using Garage_2._0.Models.Entities;
using Garage_2._0.Models.ViewModels;

namespace Garage_2._0.Services;

public interface IVehicleService
{
    int MaxCapacity { get; }

    Task<Vehicle> AddAsync(Vehicle newVehicle);
    Task CommitAsync();
    Task<Vehicle?> GetAsync(int id);
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task RemoveAsync(int id);
    Task UpdateAsync(Vehicle newVehicle);
    bool Exists(int id);
    Task<IEnumerable<Vehicle>> FilterAsync(string regNo, int? vehicleType);
    Task CheckoutAsync(Vehicle newVehicle, double parkingHourlyCost);
    Task<IEnumerable<Vehicle>> GetAllHistoryAsync();
    IEnumerable<StatisticsViewModel> GetStatistics();
    bool RegNoParked(string regNo);
    bool IsRegNoChanged(int id, string regNo);
    Task<int> CountOfVehiclesAsync();
}
