#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_2._0.Data;
using Garage_2._0.Models.Entities;
using Garage_2._0.Models.ViewModels;


namespace Garage_2._0.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage_2_0Context _context;

        public VehiclesController(Garage_2_0Context context)
        {
            _context = context;
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

            var res = await _context.Vehicle
                .Where(predicate => predicate.CheckOut == null)
                .Select(v => new OverviewModel
                {
                    Id = v.Id,
                    VehicleType = v.VehicleType,
                    RegNo = v.RegNo,
                    CheckIn = v.CheckIn,

                }).ToListAsync();

            //return View(nameof(ParkingOverView), res);
            return View("ParkingOverView", res);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var model = new ParkingDetailModel
            {
                Brand = vehicle.Brand,
                CheckIn = vehicle.CheckIn,
                CheckOut = vehicle.CheckOut,
                Id = vehicle.Id,
                Model = vehicle.Model,
                RegNo = vehicle.RegNo,
                VehicleType = vehicle.VehicleType,
                Wheels = vehicle.Wheels
            };

            return View(model);
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

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var model = new ParkingDetailModel
            {
                Id = vehicle.Id,
                RegNo = vehicle.RegNo,
                VehicleType = vehicle.VehicleType,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Wheels = vehicle.Wheels,
                CheckIn = vehicle.CheckIn,
                CheckOut = vehicle.CheckOut
            };
            return View(model);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegNo,Id,Wheels,Brand,Model,VehicleType,CheckIn,CheckOut")] ParkingDetailModel vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vehicleEntiy = new Vehicle
                    {
                        RegNo = vehicle.RegNo,
                        Brand = vehicle.Brand,
                        Model = vehicle.Model,
                        Wheels = vehicle.Wheels,
                        VehicleType = vehicle.VehicleType,
                        CheckIn = DateTime.Now
                    };
                    _context.Update(vehicleEntiy);
                    await _context.SaveChangesAsync();
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

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var parkedVehicle = new ParkingDetailModel {
                Id = vehicle.Id,
                VehicleType = vehicle.VehicleType,
                RegNo = vehicle.RegNo,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Wheels = vehicle.Wheels,
                CheckIn = vehicle.CheckIn,
                CheckOut = DateTime.Now

            };
            return View(parkedVehicle);
        }


        public async Task<IActionResult> Ticket(int? id) {
            

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var ticketVehicle = new TicketViewModel
            {
                Id = vehicle.Id,
                VehicleType = vehicle.VehicleType,
                RegNo = vehicle.RegNo,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                CheckIn = vehicle.CheckIn,
                CheckOut = DateTime.Now

            };
            return View(ticketVehicle);
        }

        

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
