using Csharp_Advanced.Models;
using Csharp_Advanced.Repositories;

namespace Csharp_Advanced.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;

        public ReservationService(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }
    }
}
