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
    public class GasStationController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public GasStationController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/GasStation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GasStation>>> GetGasStations()
        {
            return Ok(await _uow.GasStations.GetAllAsync());
        }

        // GET: api/GasStation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GasStation>> GetGasStation(Guid id)
        {
            var gasStation = await _uow.GasStations.FirstOrDefaultAsync(id);

            if (gasStation == null)
            {
                return NotFound();
            }

            return gasStation;
        }

        // PUT: api/GasStation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasStation(Guid id, GasStation gasStation)
        {
            if (id != gasStation.Id)
            {
                return BadRequest();
            }

            _uow.GasStations.Update(gasStation);

            return NoContent();
        }

        // POST: api/GasStation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GasStation>> PostGasStation(GasStation gasStation)
        {
            _uow.GasStations.Add(gasStation);

            return CreatedAtAction("GetGasStation", new { id = gasStation.Id }, gasStation);
        }

        // DELETE: api/GasStation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasStation(Guid id)
        {
            var gasStation = await _uow.GasStations.FirstOrDefaultAsync(id);
            if (gasStation == null)
            {
                return NotFound();
            }

            _uow.GasStations.Remove(gasStation);

            return NoContent();
        }

    }
}
