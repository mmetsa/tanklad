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
    public class ServiceInGasStationController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ServiceInGasStationController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/ServiceInGasStation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceInGasStation>>> GetServicesInGasStation()
        {
            return Ok(await _uow.ServicesInGasStation.GetAllAsync());
        }

        // GET: api/ServiceInGasStation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceInGasStation>> GetServiceInGasStation(Guid id)
        {
            var serviceInGasStation = await _uow.ServicesInGasStation.FirstOrDefaultAsync(id);

            if (serviceInGasStation == null)
            {
                return NotFound();
            }

            return serviceInGasStation;
        }

        // PUT: api/ServiceInGasStation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceInGasStation(Guid id, ServiceInGasStation serviceInGasStation)
        {
            if (id != serviceInGasStation.Id)
            {
                return BadRequest();
            }

            _uow.ServicesInGasStation.Update(serviceInGasStation);

            return NoContent();
        }

        // POST: api/ServiceInGasStation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceInGasStation>> PostServiceInGasStation(ServiceInGasStation serviceInGasStation)
        {
            _uow.ServicesInGasStation.Add(serviceInGasStation);

            return CreatedAtAction("GetServiceInGasStation", new { id = serviceInGasStation.Id }, serviceInGasStation);
        }

        // DELETE: api/ServiceInGasStation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceInGasStation(Guid id)
        {
            var serviceInGasStation = await _uow.ServicesInGasStation.FirstOrDefaultAsync(id);
            if (serviceInGasStation == null)
            {
                return NotFound();
            }

            _uow.ServicesInGasStation.Remove(serviceInGasStation);

            return NoContent();
        }
    }
}
