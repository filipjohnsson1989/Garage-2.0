﻿using Garage_2._0.Common;
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
    {
        _maxCapacity = 10;
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

    public async Task CheckoutAsync(Vehicle vehicleCheckout, double parkingHourlyCost)
    {
        vehicleCheckout.CheckOut = DateTime.Now;
        vehicleCheckout.ParkingCost = Util.ParkingTimeCost(vehicleCheckout.CheckIn, (DateTime)vehicleCheckout.CheckOut, parkingHourlyCost);

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

    public IEnumerable<StatisticsViewModel> GetStatistics()
    {
        var result = _context.Vehicle
         //.Where(v => !v.CheckOut.HasValue)
        .GroupBy(v => v.VehicleType)
        .Select(cv => new
        {
            VehicleType = cv.Key,
            NumOfVehicles = cv.Count(),
            TotalTime = (decimal)cv.Sum(c => EF.Functions.DateDiffMinute(c.CheckIn, c.CheckOut.HasValue ? c.CheckOut.Value : DateTime.Now)),
            NumOfWheels = cv.Sum(c => c.Wheels),
        });


        var vehicles = result.ToList().Select(v => new StatisticsViewModel()
        {
            VehicleType = v.VehicleType,
            NumOfVehicles = v.NumOfVehicles,
            TotalTime = (int)v.TotalTime,
            Payment = String.Format(" {0:C2}", Math.Round(v.TotalTime * 10 / 60, 2)),
            NumOfWheels = v.NumOfWheels,
        });

        return vehicles;

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
