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
    public class FavoriteGasStationsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public FavoriteGasStationsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: FavoriteGasStations
        public async Task<IActionResult> Index()
        {
            return View(await _uow.FavoriteGasStations.GetAllAsync());
        }

        // GET: FavoriteGasStations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteGasStation = await _uow.FavoriteGasStations.FirstOrDefaultAsync(id.Value);
            if (favoriteGasStation == null)
            {
                return NotFound();
            }

            return View(favoriteGasStation);
        }

        // GET: FavoriteGasStations/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: FavoriteGasStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FavoriteGasStation favoriteGasStation)
        {
            if (ModelState.IsValid)
            {
                favoriteGasStation.Id = Guid.NewGuid();
                _uow.FavoriteGasStations.Add(favoriteGasStation);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", favoriteGasStation.GasStationId);
            return View(favoriteGasStation);
        }

        // GET: FavoriteGasStations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteGasStation = await _uow.FavoriteGasStations.FirstOrDefaultAsync(id.Value);
            if (favoriteGasStation == null)
            {
                return NotFound();
            }
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", favoriteGasStation.GasStationId);
            return View(favoriteGasStation);
        }

        // POST: FavoriteGasStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FavoriteGasStation favoriteGasStation)
        {
            if (id != favoriteGasStation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.FavoriteGasStations.Update(favoriteGasStation);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FavoriteGasStationExists(favoriteGasStation.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", favoriteGasStation.GasStationId);
            return View(favoriteGasStation);
        }

        // GET: FavoriteGasStations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteGasStation = await _uow.FavoriteGasStations.FirstOrDefaultAsync(id.Value);
            if (favoriteGasStation == null)
            {
                return NotFound();
            }

            return View(favoriteGasStation);
        }

        // POST: FavoriteGasStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var favoriteGasStation = await _uow.FavoriteGasStations.FirstOrDefaultAsync(id);
            _uow.FavoriteGasStations.Remove(favoriteGasStation);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FavoriteGasStationExists(Guid id)
        {
            return await _uow.FavoriteGasStations.ExistsAsync(id);
        }
    }
}
