#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Garage_2._0.Models.Entities;
using Garage_2._0.Services;
using AutoMapper;
using Garage_2._0.Models.ViewModels;
using Garage_2._0.Common;

namespace Garage_2._0.Controllers;

public class VehiclesController : Controller
{
    protected readonly IMapper _mapper;
    private readonly IVehicleService _vehicleService;


    private readonly IConfiguration _config;
    private readonly double _parkingHourlyCost;


    public VehiclesController(IMapper mapper, IVehicleService vehicleService, IConfiguration config)
    {
        _mapper = mapper;
        _vehicleService = vehicleService;

        _config = config;

        if (!double.TryParse(_config["Garage:HourlyCarge"], out double timeRate))
            _parkingHourlyCost = 0.0;
        else
            _parkingHourlyCost = timeRate;
    }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {

            var res = await _context.Vehicle
                .Where(p => p.CheckOut == null)
                .Select(v => new ParkingDetailModel
                {
                    Id = v.Id,
                    Brand = v.Brand,
                    CheckIn = v.CheckIn,
                    CheckOut = v.CheckOut,
                    Model = v.Model,
                    RegNo = v.RegNo,
                    VehicleType = v.VehicleType,
                    Wheels = v.Wheels,
                }).ToListAsync();

            //return View(await _context.Vehicle.ToListAsync());
            return View(nameof(Index), res);
        }

    public async Task<IActionResult> Overview()
    {
        var vehicles = _mapper.Map<List<OverviewModel>>(await _vehicleService.GetAllAsync());
        foreach (var vehicle in vehicles)
        {
            vehicle.HourlyCost = _parkingHourlyCost;

        }
        return View("ParkingOverView", vehicles);
    }

    public async Task<IActionResult> Search(string regNo, int? vehicleType)
    {
        return View(nameof(Index), _mapper.Map<List<ParkingDetailModel>>(await _vehicleService.FilterAsync(regNo, vehicleType)));
    }

    // GET: Vehicles/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehicle = await _vehicleService.GetAsync(id.Value);
        if (vehicle == null)
        {
            return NotFound();
        }

        return View(_mapper.Map<ParkingDetailModel>(vehicle));
    }

    // GET: Vehicles/Create
    public IActionResult Create()
    {
        return View();
    }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegNo,Id,Wheels,Brand,Model,VehicleType/*,CheckIn,CheckOut*/")] ParkingDetailModel vehicle)
        {
            if (ModelState.IsValid)
            {
                //vehicle.CheckIn = DateTime.Now;
                vehicle.VehicleType = Common.VehicleTypes.Car;

                var vehicleEntiy = new Vehicle {
                    RegNo = vehicle.RegNo,
                    Brand = vehicle.Brand,
                    Model = vehicle.Model,
                    Wheels = vehicle.Wheels,
                    VehicleType = vehicle.VehicleType,
                    CheckIn = DateTime.Now
                };
                _context.Add(vehicleEntiy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

    // GET: Vehicles/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehicle = await _vehicleService.GetAsync(id.Value);
        if (vehicle == null)
        {
            return NotFound();
        }
        return View(_mapper.Map<ParkingDetailModel>(vehicle));
    }

    // POST: Vehicles/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("RegNo,Id,Wheels,Brand,Model,VehicleType,CheckIn,CheckOut")] Vehicle vehicle)
    {
        if (id != vehicle.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _vehicleService.UpdateAsync(vehicle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(vehicle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(vehicle);
    }

    // GET: Vehicles/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehicle = await _vehicleService.GetAsync(id.Value);
        vehicle.CheckOut = DateTime.Now;
        if (vehicle == null)
        {
            return NotFound();
        }

        return View(_mapper.Map<ParkingDetailModel>(vehicle));
    }

    public async Task<IActionResult> Ticket(int? id)
    {


        var vehicle = await _vehicleService.GetAsync(id.Value);
        if (vehicle == null)
        {
            return NotFound();
        }


        vehicle.CheckOut = DateTime.Now;

        return View(_mapper.Map<TicketViewModel>(vehicle));
    }

    // POST: Vehicles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _vehicleService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // GET: Vehicles/Checkout/5
    public async Task<IActionResult> Checkout(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var parkedVehicle = await _vehicleService.GetAsync(id.Value);
        parkedVehicle.CheckOut = DateTime.Now;
        if (parkedVehicle == null)
        {
            return NotFound();
        }

        return View(_mapper.Map<ParkingDetailModel>(parkedVehicle));
    }

    // POST: Vehicles/Checkout/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost, ActionName("Checkout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(int id)
    {
        try
        {
            var vehicleCheckout = await _vehicleService.GetAsync(id);
            if (vehicleCheckout == null)
                return NotFound();

            await _vehicleService.CheckoutAsync(vehicleCheckout);
            return RedirectToAction(nameof(Overview));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VehicleExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

    }

    private bool VehicleExists(int id)
    {
        return _vehicleService.Exists(id);
    }
}
