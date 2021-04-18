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
using Microsoft.AspNetCore.Authorization;
using WebApp.Controllers.Helpers;

namespace WebApp.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {

        private readonly IAppUnitOfWork _uow;

        public ContactsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Contacts
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Contacts.GetAllAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ContactTypeId"] = new SelectList(await _uow.ContactTypes.GetAllAsync(), "Id", "Name");
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name");
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Id = Guid.NewGuid();
                _uow.Contacts.Add(contact);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactTypeId"] = new SelectList(await _uow.ContactTypes.GetAllAsync(), "Id", "Name", contact.ContactTypeId);
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", contact.GasStationId);
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", contact.RetailerId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["ContactTypeId"] = new SelectList(await _uow.ContactTypes.GetAllAsync(), "Id", "Name", contact.ContactTypeId);
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", contact.GasStationId);
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", contact.RetailerId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Contacts.Update(contact);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await ContactExists(contact.Id);
                    if (!exists)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactTypeId"] = new SelectList(await _uow.ContactTypes.GetAllAsync(), "Id", "Name", contact.ContactTypeId);
            ViewData["GasStationId"] = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name", contact.GasStationId);
            ViewData["RetailerId"] = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name", contact.RetailerId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contact = await _uow.Contacts.FirstOrDefaultAsync(id);
            _uow.Contacts.Remove(contact);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ContactExists(Guid id)
        {
            return await _uow.Contacts.ExistsAsync(id);
        }
    }
}
