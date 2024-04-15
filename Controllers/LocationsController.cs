﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Csharp_Advanced.Services;
using Csharp_Advanced.Models;
using AutoMapper;

namespace Csharp_Advanced.Controllers
{
    /// <summary>
    /// Controller voor het beheren van locaties.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1")]
    public class LocationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly LocationService _locationService;
        private readonly IMapper _mapper; // Injecteer IMapper

        public LocationsController(AppDbContext context, LocationService locationService, IMapper mapper)
        {
            _context = context;
            _locationService = locationService;
            _mapper = mapper;
        }

        // GET: api/Locations
        /// <summary>
        /// Haalt alle locaties op.
        /// </summary>
        /// <returns>Een lijst met alle locaties.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
          if (_context.Locations == null)
          {
              return NotFound();
          }
            var locations = await _context.Locations
        .Include(l => l.Landlord)
            .ThenInclude(l => l.Avatar)
        .Include(l => l.Images)
        .ToListAsync();

            var locationDtos = _mapper.Map<List<LocationDto>>(locations);

            return Ok(locationDtos);
        }

        // GET: api/Locations/5
        /// <summary>
        /// Haalt een locatie op aan de hand van het ID.
        /// </summary>
        /// <param name="id">Het ID van de locatie.</param>
        /// <returns>De locatie met het opgegeven ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
          if (_context.Locations == null)
          {
              return NotFound();
          }
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // PUT: api/Locations/5
        /// <summary>
        /// Bijwerken van een locatie.
        /// </summary>
        /// <param name="id">Het ID van de locatie.</param>
        /// <param name="location">De locatiegegevens om bij te werken.</param>
        /// <returns>Een IActionResult die aangeeft of het bijwerken is geslaagd.</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        /// <summary>
        /// Toevoegen van een nieuwe locatie.
        /// </summary>
        /// <param name="location">De locatiegegevens om toe te voegen.</param>
        /// <returns>Een ActionResult met informatie over de toegevoegde locatie.</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
          if (_context.Locations == null)
          {
              return Problem("Entity set 'AppDbContext.Locations'  is null.");
          }
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        /// <summary>
        /// Verwijderen van een locatie aan de hand van het ID.
        /// </summary>
        /// <param name="id">Het ID van de locatie die moet worden verwijderd.</param>
        /// <returns>Een IActionResult die aangeeft of het verwijderen is geslaagd.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            if (_context.Locations == null)
            {
                return NotFound();
            }
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return (_context.Locations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Return all locations: api/Locations/GetAll
        /// <summary>
        /// Haalt alle locaties op.
        /// </summary>
        /// <returns>Een IActionResult met een lijst van alle locaties.</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var locations = _locationService.GetAllLocations();
            return Ok(locations);
        }

      
    }
}
