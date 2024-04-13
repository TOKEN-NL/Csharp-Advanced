
using Csharp_Advanced.Models;

namespace Csharp_Advanced.Repositories
{
    public class ReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _context.Reservations.ToList();
        }
    }
}
