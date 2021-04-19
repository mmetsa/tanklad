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
    public class RetailerController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public RetailerController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Retailer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Retailer>>> GetRetailers()
        {
            return Ok(await _uow.Retailers.GetAllAsync());
        }

        // GET: api/Retailer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Retailer>> GetRetailer(Guid id)
        {
            var retailer = await _uow.Retailers.FirstOrDefaultAsync(id);

            if (retailer == null)
            {
                return NotFound();
            }

            return retailer;
        }

        // PUT: api/Retailer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRetailer(Guid id, Retailer retailer)
        {
            if (id != retailer.Id)
            {
                return BadRequest();
            }

            _uow.Retailers.Update(retailer);

            return NoContent();
        }

        // POST: api/Retailer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Retailer>> PostRetailer(Retailer retailer)
        {
            _uow.Retailers.Add(retailer);

            return CreatedAtAction("GetRetailer", new { id = retailer.Id }, retailer);
        }

        // DELETE: api/Retailer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRetailer(Guid id)
        {
            var retailer = await _uow.Retailers.FirstOrDefaultAsync(id);
            if (retailer == null)
            {
                return NotFound();
            }

            _uow.Retailers.Remove(retailer);

            return NoContent();
        }

    }
}
