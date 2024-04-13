using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Csharp_Advanced.Services;
using Csharp_Advanced.Models;

namespace Csharp_Advanced.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandlordsController : ControllerBase
    {
        private readonly AppDbContext _context;


        public LandlordsController(AppDbContext context)
        {
            _context = context;

        }

        // GET: api/Landlords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Landlord>>> GetLandlords()
        {
          if (_context.Landlords == null)
          {
              return NotFound();
          }
            return await _context.Landlords.ToListAsync();
        }

        // GET: api/Landlords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Landlord>> GetLandlord(int id)
        {
          if (_context.Landlords == null)
          {
              return NotFound();
          }
            var landlord = await _context.Landlords.FindAsync(id);

            if (landlord == null)
            {
                return NotFound();
            }

            return landlord;
        }

        // PUT: api/Landlords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLandlord(int id, Landlord landlord)
        {
            if (id != landlord.Id)
            {
                return BadRequest();
            }

            _context.Entry(landlord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LandlordExists(id))
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

        // POST: api/Landlords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Landlord>> PostLandlord(Landlord landlord)
        {
          if (_context.Landlords == null)
          {
              return Problem("Entity set 'AppDbContext.Landlords'  is null.");
          }
            _context.Landlords.Add(landlord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLandlord", new { id = landlord.Id }, landlord);
        }

        // DELETE: api/Landlords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLandlord(int id)
        {
            if (_context.Landlords == null)
            {
                return NotFound();
            }
            var landlord = await _context.Landlords.FindAsync(id);
            if (landlord == null)
            {
                return NotFound();
            }

            _context.Landlords.Remove(landlord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LandlordExists(int id)
        {
            return (_context.Landlords?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        

    }
}
