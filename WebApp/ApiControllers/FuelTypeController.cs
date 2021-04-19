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
    public class FuelTypeController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public FuelTypeController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/FuelType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelType>>> GetFuelTypes()
        {
            return Ok(await _uow.FuelTypes.GetAllAsync());
        }

        // GET: api/FuelType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelType>> GetFuelType(Guid id)
        {
            var fuelType = await _uow.FuelTypes.FirstOrDefaultAsync(id);

            if (fuelType == null)
            {
                return NotFound();
            }

            return fuelType;
        }

        // PUT: api/FuelType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuelType(Guid id, FuelType fuelType)
        {
            if (id != fuelType.Id)
            {
                return BadRequest();
            }

            _uow.FuelTypes.Update(fuelType);

            return NoContent();
        }

        // POST: api/FuelType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FuelType>> PostFuelType(FuelType fuelType)
        {
            _uow.FuelTypes.Add(fuelType);

            return CreatedAtAction("GetFuelType", new { id = fuelType.Id }, fuelType);
        }

        // DELETE: api/FuelType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuelType(Guid id)
        {
            var fuelType = await _uow.FuelTypes.FirstOrDefaultAsync(id);
            if (fuelType == null)
            {
                return NotFound();
            }

            _uow.FuelTypes.Remove(fuelType);
            return NoContent();
        }

    }
}
