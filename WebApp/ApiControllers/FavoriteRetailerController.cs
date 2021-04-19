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
    public class FavoriteRetailerController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public FavoriteRetailerController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/FavoriteRetailer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteRetailer>>> GetFavoriteRetailers()
        {
            return Ok(await _uow.FavoriteRetailers.GetAllAsync());
        }

        // GET: api/FavoriteRetailer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteRetailer>> GetFavoriteRetailer(Guid id)
        {
            var favoriteRetailer = await _uow.FavoriteRetailers.FirstOrDefaultAsync(id);

            if (favoriteRetailer == null)
            {
                return NotFound();
            }

            return favoriteRetailer;
        }

        // PUT: api/FavoriteRetailer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteRetailer(Guid id, FavoriteRetailer favoriteRetailer)
        {
            if (id != favoriteRetailer.Id)
            {
                return BadRequest();
            }

            _uow.FavoriteRetailers.Update(favoriteRetailer);

            return NoContent();
        }

        // POST: api/FavoriteRetailer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavoriteRetailer>> PostFavoriteRetailer(FavoriteRetailer favoriteRetailer)
        {
            _uow.FavoriteRetailers.Add(favoriteRetailer);

            return CreatedAtAction("GetFavoriteRetailer", new { id = favoriteRetailer.Id }, favoriteRetailer);
        }

        // DELETE: api/FavoriteRetailer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteRetailer(Guid id)
        {
            var favoriteRetailer = await _uow.FavoriteRetailers.FirstOrDefaultAsync(id);
            if (favoriteRetailer == null)
            {
                return NotFound();
            }

            _uow.FavoriteRetailers.Remove(favoriteRetailer);

            return NoContent();
        }

    }
}
