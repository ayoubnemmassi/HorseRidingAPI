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
    [Route("[controller]")]
    [ApiController]
    public class SeancesController : ControllerBase
    {
        private readonly HorseRidingClubContext _context;

        public SeancesController(HorseRidingClubContext context)
        {
            _context = context;
        }

        // GET: api/Seances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeances()
        {
            return await _context.Seances.ToListAsync();
        }

        // GET: api/Seances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seance>> GetSeance(int id)
        {
            var seance = await _context.Seances.FindAsync(id);

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }
        // GET: api/Seances/date
        [HttpGet("getwithdate/{date}")]
        public async Task<ActionResult<Seance>> GetSeance(DateTime date)
        {
            var seance = _context.Seances
           .FromSqlRaw("SELECT * FROM seances")
           .Where(b => b.StartDate == date)
           .FirstOrDefault(); 

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }
        [HttpGet("{date}/{date1}")]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeance(DateTime date,DateTime date1)
        {
            var seance = _context.Seances
           .FromSqlRaw("SELECT * FROM seances")
           .Where(b => b.StartDate >= date&& b.StartDate<=date1)
           .ToListAsync();

            if (seance == null)
            {
                return NotFound();
            }

            return await seance;
        }
        // PUT: api/Seances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeance(int id, Seance seance)
        {
            if (id != seance.SeanceId)
            {
                return BadRequest();
            }

            _context.Entry(seance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeanceExists(id))
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

        // POST: api/Seances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Seance>> PostSeance(Seance seance)
        {
            _context.Seances.Add(seance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeance", new { id = seance.SeanceId }, seance);
        }

        // DELETE: api/Seances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Seance>> DeleteSeance(int id)
        {
            var seance = await _context.Seances.FindAsync(id);
            if (seance == null)
            {
                return NotFound();
            }

            _context.Seances.Remove(seance);
            await _context.SaveChangesAsync();

            return seance;
        }

        private bool SeanceExists(int id)
        {
            return _context.Seances.Any(e => e.SeanceId == id);
        }
    }
}
