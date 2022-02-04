#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Garage_2._0.Models.Entities;
using Garage_2._0.Services;
using AutoMapper;
using Garage_2._0.Models.ViewModels;
using Garage_2._0.Common;
using Garage_2._0.Data;

namespace Garage_2._0.Controllers;

public class VehiclesController : Controller
{
    protected readonly IMapper _mapper;
    private readonly IVehicleService _vehicleService;

   
    private readonly Garage_2_0Context db;
  

    public VehiclesController(IMapper mapper, IVehicleService vehicleService, IConfiguration config, Garage_2_0Context db)
    {
        _mapper = mapper;
        _vehicleService = vehicleService;

        
    }

    // GET: Vehicles
    public async Task<IActionResult> Index()
    {
        var vehicles = _mapper.Map<List<ParkingDetailModel>>(await _vehicleService.GetAllAsync());
        var result = new VehicleIndexViewModel()
        {
            Vehicles = vehicles,
            MaxCapacity = _vehicleService.MaxCapacity,

        };
        return View(nameof(Index), result);
    }

    public async Task<IActionResult> Statistics()
    {
        return View(_mapper.Map<List<StatisticsViewModel>>(await _vehicleService.GetStatisticsAsync()));
    }

    public async Task<IActionResult> Overview()
    {
        var vehicles = _mapper.Map<List<OverviewModel>>(await _vehicleService.GetAllAsync());
        foreach (var vehicle in vehicles)
        {
            vehicle.HourlyCost = _vehicleService.ParkingHourlyCost;

        }
        return View("ParkingOverView", vehicles);
    }

    public async Task<IActionResult> Search(string regNo, int? vehicleType, string action) 
    {
        if ((regNo == null) && (vehicleType == null))
        {
            return View("ParkingDetailModel"); 
        }
        if (action == "Index")
        {
            var query = string.IsNullOrWhiteSpace(regNo) ?
                                         db.Vehicle :
                                         db.Vehicle.Where(v => v.RegNo.StartsWith(regNo) && v.CheckOut == null);

            query = vehicleType == null ?
                             query :
                             query.Where(v => (int)v.VehicleType == vehicleType && v.CheckOut == null);

            var vehicles = await query.Where(p => p.CheckOut == null).ToListAsync();
            var viewModel = query.Select(v => new ParkingDetailModel
            {
                VehicleType = v.VehicleType,
                RegNo = v.RegNo,
                Id = v.Id,
                Wheels = v.Wheels,
                Brand = v.Brand,
                Model = v.Model,
                CheckIn = v.CheckIn,
                CheckOut = v.CheckOut

            });

            return View(nameof(Index), await viewModel.ToListAsync());
        }
        else if (action == "Overview")
        {
            var query = string.IsNullOrWhiteSpace(regNo) ?
                                          db.Vehicle :
                                          db.Vehicle.Where(v => v.RegNo.StartsWith(regNo));

            query = vehicleType == null ?
                             query :
                             query.Where(v => (int)v.VehicleType == vehicleType);


            var viewModel = query.Select(v => new OverviewModel
            {
                Id = v.Id,
                VehicleType = v.VehicleType,
                RegNo = v.RegNo,
                CheckIn = v.CheckIn
                
            });

            return View("ParkingOverView");
        }
        else if (action == "History")
        {
            var query = string.IsNullOrWhiteSpace(regNo) ?
                                          db.Vehicle :
                                          db.Vehicle.Where(v => v.RegNo.StartsWith(regNo));

            query = vehicleType == null ?
                             query :
                             query.Where(v => (int)v.VehicleType == vehicleType);


            var viewModel = query.Select(v => new HistoryViewModel
            {
                VehicleType = v.VehicleType,
                RegNo = v.RegNo,
                Id = v.Id,
                Wheels = v.Wheels,
                Brand = v.Brand,
                Model = v.Model,
                CheckIn = v.CheckIn,
                CheckOut = v.CheckOut
            });

            return View(nameof(HistoryViewModel), await viewModel.ToListAsync());
        }
        else
        {
            return View("Index");
        }
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
    public async Task<IActionResult> Create([Bind("RegNo,Id,Wheels,Brand,Model,VehicleType,Color")] Vehicle vehicle)
    {
        if (VehicleRegNoParked(vehicle.RegNo))
        {
            ModelState.AddModelError("RegNo", $"{vehicle.RegNo.ToUpper()} är redan parkerad i garaget");
            return View(_mapper.Map<ParkingDetailModel>(vehicle));
        }

        if (ModelState.IsValid)
        {

            var resultVehicle = await _vehicleService.AddAsync(vehicle);
            if (resultVehicle is null)
            {
                var CountOfVehicles = await _vehicleService.CountOfVehiclesAsync();
                //_vehicleService.MaxCapacity
                ModelState.AddModelError("Garaget är fullt.", "Garaget är fullt!");
                return View(_mapper.Map<ParkingDetailModel>(vehicle));
            }
            return RedirectToAction(nameof(Index));


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
    public async Task<IActionResult> Edit(int id, [Bind("RegNo,Id,Wheels,Brand,Model,VehicleType,Color")] Vehicle vehicle)
    {
        if (id != vehicle.Id)
        {
            return NotFound();
        }

        if (_vehicleService.IsRegNoChanged(id, vehicle.RegNo) && VehicleRegNoParked(vehicle.RegNo))
        {
            ModelState.AddModelError("RegNo", $"{vehicle.RegNo.ToUpper()} är redan parkerad i garaget");
            return View(_mapper.Map<ParkingDetailModel>(vehicle));
        }

        if (ModelState.IsValid)
        {
            try
            {
                //sätt vilka värde som skall ändrad
                //entitystate
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
            //return RedirectToAction(nameof(Index));
            return View("EditResponse", _mapper.Map<ResponseViewModel>(vehicle));
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
        ticket.HourlyCost = _vehicleService.ParkingHourlyCost;
        return View(ticket);
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

            var response = _mapper.Map<ResponseViewModel>(vehicleCheckout);
            response.HourlyCost = _vehicleService.ParkingHourlyCost;

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

    private bool VehicleRegNoParked(string regNo)
    {
        return _vehicleService.RegNoParked(regNo);
    }
}
