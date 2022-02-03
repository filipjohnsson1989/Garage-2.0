using Garage_2._0.Common;
using Garage_2._0.Data;
using Garage_2._0.Models.Entities;
using Garage_2._0.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Garage_2._0.Services;

public class VehicleService : ServiceBase, IVehicleService
{
    private readonly int _maxCapacity;
    public int MaxCapacity { get { return _maxCapacity; } }


    public VehicleService(Garage_2_0Context _context) : base(_context)
    private readonly IConfiguration _config;
    private readonly double _parkingHourlyCost;

    public double ParkingHourlyCost { get { return _parkingHourlyCost; } }

    public VehicleService(Garage_2_0Context _context, IConfiguration config) : base(_context)
    {
        _maxCapacity = 10;
        _config = config;

        if (double.TryParse(_config["Garage:HourlyCarge"], out double timeRate))
            _parkingHourlyCost = timeRate;
        else
            _parkingHourlyCost = 0.0;
    }

    public async Task<Vehicle?> AddAsync(Vehicle newVehicle)
    {
        if (_context.Vehicle.Where(v => !v.CheckOut.HasValue).Count() < _maxCapacity)
        {
            newVehicle.CheckIn = DateTime.Now;
            newVehicle.RegNo = newVehicle.RegNo.ToUpper();
            await _context.AddAsync(newVehicle);
            await _context.SaveChangesAsync();
            return newVehicle;
        }
        return null;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        var vehicles = await _context.Vehicle.Where(p => p.CheckOut == null).ToListAsync();
        return vehicles;
    }

    public async Task<IEnumerable<Vehicle>> FilterAsync(string regNo, int? vehicleType)
    {
        var query = string.IsNullOrWhiteSpace(regNo) ?
                            _context.Vehicle :
                            _context.Vehicle.Where(v => v.RegNo.StartsWith(regNo) && v.CheckOut == null);

        query = vehicleType == null ?
                         query :
                         query.Where(v => (int)v.VehicleType == vehicleType && v.CheckOut == null);

        var vehicles = await query.Where(p => p.CheckOut == null).ToListAsync();
        return vehicles;
    }

    public async Task<Vehicle?> GetAsync(int id)
    {
        var vehicle = await _context.Vehicle.FirstOrDefaultAsync(r => r.Id == id);
        return vehicle;
    }


    public async Task UpdateAsync(Vehicle newVehicle)
    {
       // var date = _context.Vehicle.AsNoTracking().FirstOrDefault...  //om jag behöver slå upp saker för att kolla ex Checkin kolla att det inter är ändrat eller ta det från db
        newVehicle.RegNo = newVehicle.RegNo.ToUpper();

        _context.Update(newVehicle);
        _context.Entry(newVehicle).Property(v => v.CheckIn).IsModified = false; //CheckIn får inte ändras
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var vehicle = await _context.Vehicle.FirstOrDefaultAsync(r => r.Id == id);
        _context.Vehicle.Remove(vehicle!);
        await _context.SaveChangesAsync();
    }

    public async Task CheckoutAsync(Vehicle vehicleCheckout)
    {
        vehicleCheckout.CheckOut = DateTime.Now;
        vehicleCheckout.ParkingCost = Util.ParkingTimeCost(vehicleCheckout.CheckIn, (DateTime)vehicleCheckout.CheckOut, _parkingHourlyCost);

        _context.Update(vehicleCheckout);
        await _context.SaveChangesAsync();
    }

    public bool Exists(int id)
    {
        return _context.Vehicle.Any(e => e.Id == id);
    }
    public bool RegNoParked(string regNo)
    {
        return _context.Vehicle.Any(e => e.CheckOut == null && e.RegNo == regNo);
    }

    public async Task<IEnumerable<Vehicle>> GetAllHistoryAsync()
    {
        var vehicleHistory = await _context.Vehicle.Where(p => p.CheckOut != null)
            .OrderBy(o => o.VehicleType)
            .ThenBy(o => o.RegNo)
            .ThenByDescending(o => o.CheckIn)
            .ToListAsync();
        return vehicleHistory;
    }

    public async Task<IEnumerable<StatisticsViewModel>> GetStatisticsAsync()
    {
        var result = _context.Vehicle
        //.Where(v => !v.CheckOut.HasValue)
        .GroupBy(v => v.VehicleType)
        .Select(cv => new StatisticsViewModel(_parkingHourlyCost)
        {
            VehicleType = cv.Key,
            NumOfVehicles = cv.Count(),
            TotalTime = cv.Sum(c => EF.Functions.DateDiffMinute(c.CheckIn, c.CheckOut.HasValue ? c.CheckOut.Value : DateTime.Now)),
            TotalUnpaidTime = cv.Sum(c => c.CheckOut.HasValue ? 0 : EF.Functions.DateDiffMinute(c.CheckIn, DateTime.Now)),
            TotalParkingCost = cv.Sum(c => c.ParkingCost ?? 0),
            NumOfWheels = cv.Sum(c => c.Wheels),
        });



        return await result.ToListAsync();

    }

    public bool IsRegNoChanged(int id, string regNo)
    {
        return _context.Vehicle.Any(e => e.CheckOut == null && e.Id == id && e.RegNo != regNo);
    }

    public async Task<int> CountOfVehiclesAsync()
    {
        return await _context.Vehicle.Where(v => !v.CheckOut.HasValue).CountAsync();
    }
}
