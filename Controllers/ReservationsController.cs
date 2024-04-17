using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Csharp_Advanced.Services;
using Csharp_Advanced.Models;
using Csharp_Advanced.DataTransferObjects;

namespace Csharp_Advanced.Controllers
{
    /// <summary>
    /// Controller voor het beheren van reserveringen.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1")]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ReservationService _reservationService;


        public ReservationsController(AppDbContext context, ReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;

        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
          if (_context.Reservations == null)
          {
              return NotFound();
          }
            return await _context.Reservations.ToListAsync();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
          if (_context.Reservations == null)
          {
              return NotFound();
          }
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Post")]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
          if (_context.Reservations == null)
          {
              return Problem("Entity set 'AppDbContext.Reservations'  is null.");
          }
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            if (_context.Reservations == null)
            {
                return NotFound();
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return (_context.Reservations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Return all reservations
        /// <summary>
        /// Haalt alle reserveringen op.
        /// </summary>
        [HttpGet("GetAllReservations")]
        public IActionResult GetAllReservations()
        {
            var reservations = _reservationService.GetAllReservations();
            return Ok(reservations);
        }
        // Maak een reservering
        /// <summary>
        /// Maakt een nieuwe reservering.
        /// </summary>
        /// <param name="requestDto">De gegevens voor het maken van de reservering.</param>
        [HttpPost]
        public async Task<ActionResult<ReservationResponseDto>> MakeReservation(ReservationRequestDto requestDto)
        {
            try
            {
                // Roep de service aan om de reservering te maken
                var reservationResponse = await _reservationService.MakeReservationAsync(requestDto);

                // Retourneer de respons van de reservering
                return Ok(reservationResponse);
            }
            catch (Exception ex)
            {
                // Als er een fout optreedt, retourneer dan een foutreactie
                return StatusCode(StatusCodes.Status500InternalServerError, $"Er is een fout opgetreden bij het maken van de reservering: {ex.Message}");
            }
        }

    }
}
