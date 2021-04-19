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

    public class CustomerCardsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CustomerCardsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/CustomerCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerCard>>> GetCustomerCards()
        {
            return Ok(await _uow.CustomerCards.GetAllAsync());
        }

        // GET: api/CustomerCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerCard>> GetCustomerCard(Guid id)
        {
            var customerCard = await _uow.CustomerCards.FirstOrDefaultAsync(id);

            if (customerCard == null)
            {
                return NotFound();
            }

            return customerCard;
        }

        // PUT: api/CustomerCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerCard(Guid id, CustomerCard customerCard)
        {
            if (id != customerCard.Id)
            {
                return BadRequest();
            }

            _uow.CustomerCards.Update(customerCard);

            return NoContent();
        }

        // POST: api/CustomerCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerCard>> PostCustomerCard(CustomerCard customerCard)
        {
            _uow.CustomerCards.Add(customerCard);

            return CreatedAtAction("GetCustomerCard", new { id = customerCard.Id }, customerCard);
        }

        // DELETE: api/CustomerCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerCard(Guid id)
        {
            var customerCard = await _uow.CustomerCards.FirstOrDefaultAsync(id);
            if (customerCard == null)
            {
                return NotFound();
            }

            _uow.CustomerCards.Remove(customerCard);

            return NoContent();
        }
    }
}
