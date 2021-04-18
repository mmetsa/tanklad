using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Controllers
{
    public class FuelTypeInGasStationsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public FuelTypeInGasStationsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: FuelTypeInGasStations
        public async Task<IActionResult> Index()
        {
            return View(await _uow.FuelTypesInGasStation.GetAllAsync());
        }

        // GET: FuelTypeInGasStations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelTypeInGasStation = await _uow.FuelTypesInGasStation.FirstOrDefaultAsync(id.Value);
            if (fuelTypeInGasStation == null)
            {
                return NotFound();
            }

            return View(fuelTypeInGasStation);
        }

        // GET: FuelTypeInGasStations/Create
        public async Task<IActionResult> Create()
        {
            ViewData["FuelTypeId"] = new SelectList(await _uow.FuelTypes.GetAllAsync(), "Id", "Name");
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: FuelTypeInGasStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,Price,GasStationId,FuelTypeId,Id")] FuelTypeInGasStation fuelTypeInGasStation)
        {
            if (ModelState.IsValid)
            {
                fuelTypeInGasStation.Id = Guid.NewGuid();
                _uow.FuelTypesInGasStation.Add(fuelTypeInGasStation);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuelTypeId"] = new SelectList(await _uow.FuelTypes.GetAllAsync(), "Id", "Name", fuelTypeInGasStation.FuelTypeId);
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", fuelTypeInGasStation.GasStationId);
            return View(fuelTypeInGasStation);
        }

        // GET: FuelTypeInGasStations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelTypeInGasStation = await _uow.FuelTypesInGasStation.FirstOrDefaultAsync(id.Value);
            if (fuelTypeInGasStation == null)
            {
                return NotFound();
            }
            ViewData["FuelTypeId"] = new SelectList(await _uow.FuelTypes.GetAllAsync(), "Id", "Name", fuelTypeInGasStation.FuelTypeId);
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", fuelTypeInGasStation.GasStationId);
            return View(fuelTypeInGasStation);
        }

        // POST: FuelTypeInGasStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StartTime,EndTime,Price,GasStationId,FuelTypeId,Id")] FuelTypeInGasStation fuelTypeInGasStation)
        {
            if (id != fuelTypeInGasStation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.FuelTypesInGasStation.Update(fuelTypeInGasStation);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FuelTypeInGasStationExists(fuelTypeInGasStation.Id))
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
            ViewData["FuelTypeId"] = new SelectList(await _uow.FuelTypes.GetAllAsync(), "Id", "Name", fuelTypeInGasStation.FuelTypeId);
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", fuelTypeInGasStation.GasStationId);
            return View(fuelTypeInGasStation);
        }

        // GET: FuelTypeInGasStations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelTypeInGasStation = await _uow.FuelTypesInGasStation.FirstOrDefaultAsync(id.Value);
            if (fuelTypeInGasStation == null)
            {
                return NotFound();
            }

            return View(fuelTypeInGasStation);
        }

        // POST: FuelTypeInGasStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fuelTypeInGasStation = await _uow.FuelTypesInGasStation.FirstOrDefaultAsync(id);
            _uow.FuelTypesInGasStation.Remove(fuelTypeInGasStation);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FuelTypeInGasStationExists(Guid id)
        {
            return await _uow.FuelTypesInGasStation.ExistsAsync(id);
        }
    }
}
