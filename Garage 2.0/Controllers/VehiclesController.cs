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

        if (double.TryParse(_config["Garage:HourlyCarge"], out double timeRate))
            _parkingHourlyCost = timeRate;
        else
            _parkingHourlyCost = 0.0;
    }

    // GET: Vehicles
    public async Task<IActionResult> Index()
    {
        return View(nameof(Index), _mapper.Map<List<ParkingDetailModel>>(await _vehicleService.GetAllAsync()));
    }

    public IActionResult Statistics()
    {
        return View(_mapper.Map<List<StatisticsViewModel>>(_vehicleService.GetStatistics()));
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
    public async Task<IActionResult> Create([Bind("RegNo,Id,Wheels,Brand,Model,VehicleType")] Vehicle vehicle)
    {
        if (ModelState.IsValid)
        {
            await _vehicleService.AddAsync(vehicle);
            return View("CheckInResponse", _mapper.Map<ParkingDetailModel>(vehicle));
            //return RedirectToAction(nameof(Index));
        }
        return View(_mapper.Map<ParkingDetailModel>(vehicle));
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

        var ticket = _mapper.Map<TicketViewModel>(vehicle);
        ticket.HourlyCost = _parkingHourlyCost;
        return View(ticket);
    }

    // POST: Vehicles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _vehicleService.RemoveAsync(id);
        return RedirectToAction(nameof(History));
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

           await _vehicleService.CheckoutAsync(vehicleCheckout, _parkingHourlyCost);

            var response = _mapper.Map<ResponseViewModel>(vehicleCheckout);
            response.HourlyCost = _parkingHourlyCost;

            return View("CheckoutResponse", response);
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
    public async Task<IActionResult> History()
    {
        return View(nameof(History), _mapper.Map<List<HistoryViewModel>>(await _vehicleService.GetAllHistoryAsync()));
    }

    private bool VehicleExists(int id)
    {
        return _vehicleService.Exists(id);
    }
}
