using System;
using System.Threading.Tasks;
using Csharp_Advanced.DataTransferObjects;
using Csharp_Advanced.Models;
using Csharp_Advanced.Repositories;

namespace Csharp_Advanced.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly LocationService _locationService;
        private readonly LocationRepository _locationRepository;



        public ReservationService(ReservationRepository reservationRepository, CustomerRepository customerRepository, LocationService locationService, LocationRepository locationRepository)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _locationService = locationService;
            _locationRepository = locationRepository;
        }
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }
            public async Task<ReservationResponseDto> MakeReservationAsync(ReservationRequestDto requestDto)
        {
            // Controleer beschikbaarheid
            bool isAvailable = await CheckAvailabilityAsync(requestDto.LocationId, requestDto.StartDate, requestDto.EndDate);
            if (!isAvailable)
            {
                throw new InvalidOperationException("De opgegeven datums zijn niet beschikbaar voor reservering.");
            }

            // Zoek de klant op basis van e-mailadres
            Customer customer = await _customerRepository.GetCustomerByEmailAsync(requestDto.Email);
            if (customer == null)
            {
                // Maak de klant aan als deze niet bestaat
                customer = new Customer
                {
                    Email = requestDto.Email,
                    FirstName = requestDto.FirstName,
                    LastName = requestDto.LastName
                };
                await _customerRepository.AddCustomerAsync(customer);
            }

            // Maak de reservering aan
            Reservation reservation = new Reservation
            {
                StartDate = requestDto.StartDate,
                EndDate = requestDto.EndDate,
                Location = await _locationRepository.GetLocationByIdAsync(requestDto.LocationId),
                Customer = customer,
                Discount = requestDto.Discount ?? 0 // Standaard 0 als er geen korting wordt opgegeven
            };
            await _reservationRepository.AddReservationAsync(reservation);

            // Bereken de prijs van de reservering
            float price = await CalculateReservationPrice(requestDto.StartDate, requestDto.EndDate, reservation.Location.Id, reservation.Discount);

            // Maak de responsedto aan
            ReservationResponseDto responseDto = new ReservationResponseDto
            {
                LocationName = reservation.Location.Title, 
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                Price = price,
                Discount = reservation.Discount
            };

            return responseDto;
        }

        // Methode om de prijs van de reservering te berekenen
        private async Task<float> CalculateReservationPrice(DateTime startDate, DateTime endDate, int locationId, float discount)
        {
            // Haal de locatie op basis van het locatie-ID
            var location = await _locationRepository.GetLocationByIdAsync(locationId);

            // Bereken het aantal dagen van de reservering
            int numberOfDays = (int)(endDate - startDate).TotalDays;

            // Bereken de prijs van de reservering zonder korting
            float totalPrice = numberOfDays * location.PricePerDay;

            // Pas de korting toe indien aanwezig
            if (discount > 0 && discount <= 1)
            {
                totalPrice -= totalPrice * discount; 
            }

            return totalPrice;
        }

        public async Task<bool> CheckAvailabilityAsync(int locationId, DateTime startDate, DateTime endDate)
        {
            // Haal alle onbeschikbare datums op voor de opgegeven locatie
            var unAvailableDates = await _locationService.GetUnAvailableDatesAsync(locationId);

            // Controleer of er overlap is tussen de gewenste periode en de onbeschikbare datums
            foreach (var date in Enumerable.Range(0, (int)(endDate - startDate).TotalDays + 1).Select(offset => startDate.AddDays(offset)))
            {
                if (unAvailableDates.Contains(date))
                {
                    // Er is overlap gevonden, de locatie is niet beschikbaar voor deze periode
                    return false;
                }
            }

            // Er zijn geen conflicten gevonden, de locatie is beschikbaar voor de gewenste periode
            return true;
        }

    }
}
