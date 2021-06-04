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
        [HttpGet("getwithdate/{date}")]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeance(DateTime date)
        {
            var seance =  _context.Seances
           .FromSqlRaw("SELECT * FROM seances")
           .Where(b => b.StartDate.Date == date )
           .ToList();

            if (seance == null)
            {
                return NotFound();
            }

            return  seance;
        }
        [HttpGet("getwithfulldate/{date}")]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeances(DateTime date)
        {
            var seance = _context.Seances
           .FromSqlRaw("SELECT * FROM seances ")
           .Where(b => b.StartDate == date)
           .ToList();

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }
        [HttpGet("names/{id}")]
        public  ActionResult<MonitorXClient> Getnames(int id)
        {
            var req = (from S in _context.Seances where S.SeanceId==id
                       select new MonitorXClient
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
                          


                       }).FirstOrDefault();

            return req;
        } 
        
        [HttpGet("allnames/{date}")]
        public  ActionResult<IEnumerable<MonitorXClient>> Getnames(DateTime date)
        {
            var req = (from S in _context.Seances where S.StartDate==date
                       select new MonitorXClient
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
                          


                       }).ToList();

            return req;
        }
        [HttpGet("allnamesClient/{date}/{id}")]
        public  ActionResult<IEnumerable<MonitorXClient>> Getnames(DateTime date,int id)
        {
            var req = (from S in _context.Seances where S.StartDate==date && S.ClientId==id
                       select new MonitorXClient
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
                          


                       }).ToList();

            return req;
        }    [HttpGet("allnamesMonitor/{date}/{id}")]
        public  ActionResult<IEnumerable<MonitorXClient>> GetnamesMonitor(DateTime date,int id)
        {
            var req = (from S in _context.Seances where S.StartDate==date && S.MonitorId==id
                       select new MonitorXClient
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
                          


                       }).ToList();

            return req;
        }
        [HttpGet("allnamesshortdate/{date}")]
        public  ActionResult<IEnumerable<MonitorXClient>> Getnamesshortdate(DateTime date)
        {
            var req = (from S in _context.Seances where S.StartDate.Date==date
                       select new MonitorXClient
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
                          


                       }).ToList();

            return req;
        }  
        [HttpGet("allnamesshortdate/{date}/{id}")]
        public  ActionResult<IEnumerable<MonitorXClient>> Getnamesshortdate(DateTime date,int id)
        {
            var req = (from S in _context.Seances where S.StartDate.Date==date && S.ClientId==id
                       select new MonitorXClient
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
                          


                       }).ToList();

            return req;
        }        [HttpGet("allnamesshortdateMonitor/{date}/{id}")]
        public  ActionResult<IEnumerable<MonitorXClient>> GetnamesshortdateMonitor(DateTime date,int id)
        {
            var req = (from S in _context.Seances where S.StartDate.Date==date && S.MonitorId==id
                       select new MonitorXClient
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
                          


                       }).ToList();

            return req;
        }
        [HttpGet("getwithfulldatemonitor/{date}")]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeancesandMonitor(DateTime date)
        {
            var seance = _context.Seances
           .FromSqlRaw("SELECT * FROM seances s join users u on s.MonitorId=u.UserId")
           .Where(b => b.StartDate == date)
           .ToList();

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }
        // GET: api/Seances/date
        [HttpGet("getwithdate/{date}/{id}")]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeance(DateTime date,int id )
        {
            var seance = _context.Seances
           .FromSqlRaw("SELECT * FROM seances")
           .Where(b => b.StartDate.Date== date&& b.ClientId==id)
           .ToList(); 

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }   // GET: monitor/date/id
        [HttpGet("monitor/{date}/{id}")]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeanceMonitor(DateTime date,int id )
        {
            var seance = _context.Seances
           .FromSqlRaw("SELECT * FROM seances")
           .Where(b => b.StartDate.Date== date&& b.MonitorId==id)
           .ToList(); 

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }
        [HttpGet("{date}/{date1}")]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeance(DateTime date, DateTime date1)
        {
            var seance = _context.Seances
           .FromSqlRaw("SELECT * FROM seances")
           .Where(b => (b.StartDate >= date && b.StartDate <= date1) )
           .ToListAsync();

            if (seance == null)
            {
                return NotFound();
            }

            return await seance;
        }
        [HttpGet("{date}/{date1}/{id}")]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeance(DateTime date,DateTime date1,int id)
        {
            var seance = _context.Seances
           .FromSqlRaw("SELECT * FROM seances")
           .Where(b =>( b.StartDate >= date&& b.StartDate<=date1)&&b.ClientId==id)
           .ToListAsync();

            if (seance == null)
            {
                return NotFound();
            }

            return await seance;
        }
        [HttpGet("monitor/{date}/{date1}/{id}")]
        public async Task<ActionResult<IEnumerable<Seance>>> GetMonitorSeance(DateTime date, DateTime date1, int id)
        {
            var seance = _context.Seances
           .FromSqlRaw("SELECT * FROM seances")
           .Where(b => (b.StartDate >= date && b.StartDate <= date1) && b.MonitorId == id)
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
