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
    public class FuelTypeInGasStationController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public FuelTypeInGasStationController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/FuelTypeInGasStation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelTypeInGasStation>>> GetFuelTypesInGasStation()
        {
            return Ok(await _uow.FuelTypesInGasStation.GetAllAsync());
        }

        // GET: api/FuelTypeInGasStation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelTypeInGasStation>> GetFuelTypeInGasStation(Guid id)
        {
            var fuelTypeInGasStation = await _uow.FuelTypesInGasStation.FirstOrDefaultAsync(id);

            if (fuelTypeInGasStation == null)
            {
                return NotFound();
            }

            return fuelTypeInGasStation;
        }

        // PUT: api/FuelTypeInGasStation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuelTypeInGasStation(Guid id, FuelTypeInGasStation fuelTypeInGasStation)
        {
            if (id != fuelTypeInGasStation.Id)
            {
                return BadRequest();
            }

            _uow.FuelTypesInGasStation.Update(fuelTypeInGasStation);

            return NoContent();
        }

        // POST: api/FuelTypeInGasStation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FuelTypeInGasStation>> PostFuelTypeInGasStation(FuelTypeInGasStation fuelTypeInGasStation)
        {
            _uow.FuelTypesInGasStation.Add(fuelTypeInGasStation);

            return CreatedAtAction("GetFuelTypeInGasStation", new { id = fuelTypeInGasStation.Id }, fuelTypeInGasStation);
        }

        // DELETE: api/FuelTypeInGasStation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuelTypeInGasStation(Guid id)
        {
            var fuelTypeInGasStation = await _uow.FuelTypesInGasStation.FirstOrDefaultAsync(id);
            if (fuelTypeInGasStation == null)
            {
                return NotFound();
            }

            _uow.FuelTypesInGasStation.Remove(fuelTypeInGasStation);

            return NoContent();
        }

    }
}
