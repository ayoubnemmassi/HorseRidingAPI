using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorseRidingAPI.Models;

namespace HorseRidingAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly HorseRidingClubContext _context;

        public ClientsController(HorseRidingClubContext context)
        {
            _context = context;
        }

        // GET: api/Clients

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }     [HttpGet("my/{id}")]
        public ActionResult<IEnumerable<MonitorXClient>> Getnames(int id)
        {
            var req = (from S in _context.Seances
                       where S.MonitorId == id
                       select  new MonitorXClient
                       {
                           SeanceId = S.SeanceId,
                           ClientId = (from c in _context.Clients where c.ClientId == S.ClientId select c.FName).FirstOrDefault() + " " + (from c in _context.Clients where c.ClientId == S.ClientId select c.LName).FirstOrDefault(),
                           MonitorId = (from c in _context.Users where c.UserId == S.MonitorId select c.UserFname).FirstOrDefault() + " " + (from c in _context.Users where c.UserId == S.MonitorId select c.UserLname).FirstOrDefault(),
                           Comments = S.Comments,
                           DurationMinut = S.DurationMinut,
                           IsDone = S.DurationMinut,
                           SeanceGrpId = S.SeanceGrpId,
                           StartDate = S.StartDate,
                           PaymentId = S.PaymentId,
                           Client=S.Client


                       } ).ToList();

            return req;
        }
        // PUT: api/Clients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Clients.Add(client);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(client.ClientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return client;
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
