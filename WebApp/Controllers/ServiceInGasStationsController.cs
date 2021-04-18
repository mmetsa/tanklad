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
    public class ServiceInGasStationsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ServiceInGasStationsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ServiceInGasStations
        public async Task<IActionResult> Index()
        {
            return View(await _uow.ServicesInGasStation.GetAllAsync());
        }

        // GET: ServiceInGasStations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceInGasStation = await _uow.ServicesInGasStation.FirstOrDefaultAsync(id.Value);
            if (serviceInGasStation == null)
            {
                return NotFound();
            }

            return View(serviceInGasStation);
        }

        // GET: ServiceInGasStations/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name");
            ViewData["ServiceId"] = new SelectList(await _uow.Services.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: ServiceInGasStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,GasStationId,ServiceId,Id")] ServiceInGasStation serviceInGasStation)
        {
            if (ModelState.IsValid)
            {
                serviceInGasStation.Id = Guid.NewGuid();
                _uow.ServicesInGasStation.Add(serviceInGasStation);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", serviceInGasStation.GasStationId);
            ViewData["ServiceId"] = new SelectList(await _uow.Services.GetAllAsync(), "Id", "Name", serviceInGasStation.ServiceId);
            return View(serviceInGasStation);
        }

        // GET: ServiceInGasStations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceInGasStation = await _uow.ServicesInGasStation.FirstOrDefaultAsync(id.Value);
            if (serviceInGasStation == null)
            {
                return NotFound();
            }
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", serviceInGasStation.GasStationId);
            ViewData["ServiceId"] = new SelectList(await _uow.Services.GetAllAsync(), "Id", "Name", serviceInGasStation.ServiceId);
            return View(serviceInGasStation);
        }

        // POST: ServiceInGasStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StartTime,EndTime,GasStationId,ServiceId,Id")] ServiceInGasStation serviceInGasStation)
        {
            if (id != serviceInGasStation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.ServicesInGasStation.Update(serviceInGasStation);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ServiceInGasStationExists(serviceInGasStation.Id))
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
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", serviceInGasStation.GasStationId);
            ViewData["ServiceId"] = new SelectList(await _uow.Services.GetAllAsync(), "Id", "Name", serviceInGasStation.ServiceId);
            return View(serviceInGasStation);
        }

        // GET: ServiceInGasStations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceInGasStation = await _uow.ServicesInGasStation.FirstOrDefaultAsync(id.Value);
            if (serviceInGasStation == null)
            {
                return NotFound();
            }

            return View(serviceInGasStation);
        }

        // POST: ServiceInGasStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var serviceInGasStation = await _uow.ServicesInGasStation.FirstOrDefaultAsync(id);
            _uow.ServicesInGasStation.Remove(serviceInGasStation);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ServiceInGasStationExists(Guid id)
        {
            return await _uow.ServicesInGasStation.ExistsAsync(id);
        }
    }
}
