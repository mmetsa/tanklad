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
    public class FavoriteRetailersController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public FavoriteRetailersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: FavoriteRetailers
        public async Task<IActionResult> Index()
        {
            return View(await _uow.FavoriteRetailers.GetAllAsync());
        }

        // GET: FavoriteRetailers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteRetailer = await _uow.FavoriteRetailers.FirstOrDefaultAsync(id.Value);
            if (favoriteRetailer == null)
            {
                return NotFound();
            }

            return View(favoriteRetailer);
        }

        // GET: FavoriteRetailers/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: FavoriteRetailers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,RetailerId,Id")] FavoriteRetailer favoriteRetailer)
        {
            if (ModelState.IsValid)
            {
                favoriteRetailer.Id = Guid.NewGuid();
                _uow.FavoriteRetailers.Add(favoriteRetailer);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", favoriteRetailer.RetailerId);
            return View(favoriteRetailer);
        }

        // GET: FavoriteRetailers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteRetailer = await _uow.FavoriteRetailers.FirstOrDefaultAsync(id.Value);
            if (favoriteRetailer == null)
            {
                return NotFound();
            }
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", favoriteRetailer.RetailerId);
            return View(favoriteRetailer);
        }

        // POST: FavoriteRetailers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StartTime,EndTime,RetailerId,Id")] FavoriteRetailer favoriteRetailer)
        {
            if (id != favoriteRetailer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.FavoriteRetailers.Update(favoriteRetailer);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FavoriteRetailerExists(favoriteRetailer.Id))
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
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", favoriteRetailer.RetailerId);
            return View(favoriteRetailer);
        }

        // GET: FavoriteRetailers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteRetailer = await _uow.FavoriteRetailers.FirstOrDefaultAsync(id.Value);
            if (favoriteRetailer == null)
            {
                return NotFound();
            }

            return View(favoriteRetailer);
        }

        // POST: FavoriteRetailers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var favoriteRetailer = await _uow.FavoriteRetailers.FirstOrDefaultAsync(id);
            _uow.FavoriteRetailers.Remove(favoriteRetailer);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FavoriteRetailerExists(Guid id)
        {
            return await _uow.FavoriteRetailers.ExistsAsync(id);
        }
    }
}
