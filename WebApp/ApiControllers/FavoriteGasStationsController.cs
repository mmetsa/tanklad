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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FavoriteGasStationsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public FavoriteGasStationsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/FavoriteGasStations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteGasStation>>> GetFavoriteGasStations()
        {
            return Ok(await _uow.FavoriteGasStations.GetAllAsync());
        }

        // GET: api/FavoriteGasStations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteGasStation>> GetFavoriteGasStation(Guid id)
        {
            var favoriteGasStation = await _uow.FavoriteGasStations.FirstOrDefaultAsync(id);

            if (favoriteGasStation == null)
            {
                return NotFound();
            }

            return favoriteGasStation;
        }

        // PUT: api/FavoriteGasStations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteGasStation(Guid id, FavoriteGasStation favoriteGasStation)
        {
            if (id != favoriteGasStation.Id)
            {
                return BadRequest();
            }

            _uow.FavoriteGasStations.Update(favoriteGasStation);

            return NoContent();
        }

        // POST: api/FavoriteGasStations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavoriteGasStation>> PostFavoriteGasStation(FavoriteGasStation favoriteGasStation)
        {
            _uow.FavoriteGasStations.Add(favoriteGasStation);

            return CreatedAtAction("GetFavoriteGasStation", new { id = favoriteGasStation.Id }, favoriteGasStation);
        }

        // DELETE: api/FavoriteGasStations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteGasStation(Guid id)
        {
            var favoriteGasStation = await _uow.FavoriteGasStations.FirstOrDefaultAsync(id);
            if (favoriteGasStation == null)
            {
                return NotFound();
            }

            _uow.FavoriteGasStations.Remove(favoriteGasStation);

            return NoContent();
        }

    }
}
