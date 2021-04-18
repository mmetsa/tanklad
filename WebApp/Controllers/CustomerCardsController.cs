using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;

namespace WebApp.Controllers
{
    public class CustomerCardsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CustomerCardsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CustomerCards
        public async Task<IActionResult> Index()
        {
            return View(await _uow.CustomerCards.GetAllAsync());
        }

        // GET: CustomerCards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCard = await _uow.CustomerCards.FirstOrDefaultAsync(id.Value);
            if (customerCard == null)
            {
                return NotFound();
            }

            return View(customerCard);
        }

        // GET: CustomerCards/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: CustomerCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Discount,Description,RetailerId,Id")] CustomerCard customerCard)
        {
            if (ModelState.IsValid)
            {
                customerCard.Id = Guid.NewGuid();
                _uow.CustomerCards.Add(customerCard);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", customerCard.RetailerId);
            return View(customerCard);
        }

        // GET: CustomerCards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCard = await _uow.CustomerCards.FirstOrDefaultAsync(id.Value);
            if (customerCard == null)
            {
                return NotFound();
            }
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", customerCard.RetailerId);
            return View(customerCard);
        }

        // POST: CustomerCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Discount,Description,RetailerId,Id")] CustomerCard customerCard)
        {
            if (id != customerCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.CustomerCards.Update(customerCard);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CustomerCardExists(customerCard.Id))
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
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", customerCard.RetailerId);
            return View(customerCard);
        }

        // GET: CustomerCards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCard = await _uow.CustomerCards.FirstOrDefaultAsync(id.Value);
            if (customerCard == null)
            {
                return NotFound();
            }

            return View(customerCard);
        }

        // POST: CustomerCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customerCard = await _uow.CustomerCards.FirstOrDefaultAsync(id);
            _uow.CustomerCards.Remove(customerCard);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CustomerCardExists(Guid id)
        {
            return await _uow.CustomerCards.ExistsAsync(id);
        }
    }
}
