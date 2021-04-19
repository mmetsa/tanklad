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
using WebApp.ViewModels;

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
            var vm = new ContactCEViewModel();
            vm.ContactTypeSelectList = new SelectList(await _uow.ContactTypes.GetAllAsync(), "Id", "Name");
            vm.GasStationSelectList = new SelectList(await _uow.GasStations.GetAllAsync(), "Id", "Name");
            vm.RetailerSelectList = new SelectList(await _uow.Retailers.GetAllAsync(), "Id", "Name");
            return View(vm);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ContactCEViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Contact.Id = Guid.NewGuid();
                _uow.Contacts.Add(vm.Contact);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.ContactTypeSelectList = new SelectList(await _uow.ContactTypes.GetAllAsync(), nameof(ContactType.Id), nameof(ContactType.Name), vm.Contact.ContactTypeId);
            vm.GasStationSelectList = new SelectList(await _uow.GasStations.GetAllAsync(), nameof(GasStation.Id), nameof(GasStation.Name), vm.Contact.GasStationId);
            vm.RetailerSelectList = new SelectList(await _uow.Retailers.GetAllAsync(), nameof(Retailer.Id), nameof(Retailer.Name), vm.Contact.RetailerId);
            return View(vm);
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
            var vm = new ContactCEViewModel();
            vm.Contact = contact;
            vm.ContactTypeSelectList = new SelectList(await _uow.ContactTypes.GetAllAsync(), nameof(ContactType.Id), nameof(ContactType.Name), vm.Contact.ContactTypeId);
            vm.GasStationSelectList = new SelectList(await _uow.GasStations.GetAllAsync(), nameof(GasStation.Id), nameof(GasStation.Name), vm.Contact.GasStationId);
            vm.RetailerSelectList = new SelectList(await _uow.Retailers.GetAllAsync(), nameof(Retailer.Id), nameof(Retailer.Name), vm.Contact.RetailerId);
            return View(vm);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ContactCEViewModel vm)
        {
            if (id != vm.Contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Contacts.Update(vm.Contact);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await ContactExists(vm.Contact.Id);
                    if (!exists)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            vm.ContactTypeSelectList = new SelectList(await _uow.ContactTypes.GetAllAsync(), nameof(ContactType.Id), nameof(ContactType.Name), vm.Contact.ContactTypeId);
            vm.GasStationSelectList = new SelectList(await _uow.GasStations.GetAllAsync(), nameof(GasStation.Id), nameof(GasStation.Name), vm.Contact.GasStationId);
            vm.RetailerSelectList = new SelectList(await _uow.Retailers.GetAllAsync(), nameof(Retailer.Id), nameof(Retailer.Name), vm.Contact.RetailerId);
            return View(vm);
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
