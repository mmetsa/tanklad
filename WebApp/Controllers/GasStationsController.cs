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
    public class GasStationsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public GasStationsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: GasStations
        public async Task<IActionResult> Index()
        {
            return View(await _uow.GasStations.GetAllAsync());
        }

        // GET: GasStations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasStation = await _uow.GasStations.FirstOrDefaultAsync(id.Value);
            if (gasStation == null)
            {
                return NotFound();
            }

            return View(gasStation);
        }

        // GET: GasStations/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: GasStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Latitude,Longitude,Address,OpenHours,RetailerId,Id")] GasStation gasStation)
        {
            if (ModelState.IsValid)
            {
                gasStation.Id = Guid.NewGuid();
                _uow.GasStations.Add(gasStation);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", gasStation.RetailerId);
            return View(gasStation);
        }

        // GET: GasStations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasStation = await _uow.GasStations.FirstOrDefaultAsync(id.Value);
            if (gasStation == null)
            {
                return NotFound();
            }
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", gasStation.RetailerId);
            return View(gasStation);
        }

        // POST: GasStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Latitude,Longitude,Address,OpenHours,RetailerId,Id")] GasStation gasStation)
        {
            if (id != gasStation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.GasStations.Update(gasStation);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GasStationExists(gasStation.Id))
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
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", gasStation.RetailerId);
            return View(gasStation);
        }

        // GET: GasStations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasStation = await _uow.GasStations.FirstOrDefaultAsync(id.Value);
            if (gasStation == null)
            {
                return NotFound();
            }

            return View(gasStation);
        }

        // POST: GasStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gasStation = await _uow.GasStations.FirstOrDefaultAsync(id);
            _uow.GasStations.Remove(gasStation);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GasStationExists(Guid id)
        {
            return await _uow.GasStations.ExistsAsync(id);
        }
    }
}
