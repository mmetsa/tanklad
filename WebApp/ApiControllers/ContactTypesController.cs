using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTypesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ContactTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/ContactTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactType>>> GetContactTypes()
        {
            return Ok(await _uow.ContactTypes.GetAllAsync());
        }

        // GET: api/ContactTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactType>> GetContactType(Guid id)
        {
            var contactType = await _uow.ContactTypes.FirstOrDefaultAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return contactType;
        }

        // PUT: api/ContactTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactType(Guid id, ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest();
            }

            var cType = await _uow.ContactTypes.FirstOrDefaultAsync(id);
            if (cType == null)
            {
                return NotFound();
            }

            _uow.ContactTypes.Update(contactType);

            return NoContent();
        }

        // POST: api/ContactTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactType>> PostContactType(ContactType contactType)
        {
            _uow.ContactTypes.Add(contactType);

            return CreatedAtAction("GetContactType", new { id = contactType.Id }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactType(Guid id)
        {
            var contactType = await _uow.ContactTypes.FirstOrDefaultAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            _uow.ContactTypes.Remove(contactType);

            return NoContent();
        }
    }
}
