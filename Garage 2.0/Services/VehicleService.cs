using AutoMapper;
using Garage_2._0.Data;
using Garage_2._0.Models.Entities;
using Garage_2._0.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Garage_2._0.Services;

public class VehicleService : ServiceBase, IVehicleService
{
    public VehicleService(IMapper _mapper, AppDbContext _context) : base(_mapper, _context)
    {
    }

    public async Task<VehicleIndexViewModel> AddAsync(Vehicle newVehicle)
    {
        await _context.AddAsync(newVehicle);
        await _context.SaveChangesAsync();
        return _mapper.Map<VehicleIndexViewModel>(newVehicle);
    }


    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<VehicleIndexViewModel?> GetAsync(int id)
    {
        var vehicle= await _context.Vehicle.FirstOrDefaultAsync(r => r.Id == id);
        return _mapper.Map<VehicleIndexViewModel>(vehicle);
    }

    public async Task<IEnumerable<VehicleIndexViewModel>> GetAllAsync()
    {
        var vehicles = await _context.Vehicle.ToListAsync();
        return _mapper.Map<List<VehicleIndexViewModel>>(vehicles);
    }

    public async Task UpdateAsync(Vehicle newVehicle)
    {
        _context.Update(newVehicle);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var vehicle = await _context.Vehicle.FirstOrDefaultAsync(r => r.Id == id);
        _context.Vehicle.Remove(vehicle!);
        await _context.SaveChangesAsync();
    }

    public bool Exists(int id)
    {
        return _context.Vehicle.Any(e => e.Id == id);
    }


}
