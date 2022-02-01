using Garage_2._0.Models.Entities;
using Garage_2._0.Models.ViewModels;

namespace Garage_2._0.Services;

public interface IVehicleService
{
    Task<VehicleIndexViewModel> AddAsync(Vehicle newVehicle);
    Task CommitAsync();
    Task<VehicleIndexViewModel?> GetAsync(int id);
    Task<IEnumerable<VehicleIndexViewModel>> GetAllAsync();
    Task RemoveAsync(int id);
    Task UpdateAsync(Vehicle newVehicle);
    bool Exists(int id);
}
