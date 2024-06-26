﻿using Csharp_Advanced.DataTransferObjects;
using Csharp_Advanced.Models;
using Csharp_Advanced.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Threading;

namespace Csharp_Advanced.Services
{
    public class LocationService
    {
        private readonly LocationRepository _locationRepository;

        public LocationService(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;

        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync(CancellationToken cancellationToken = default)
        {
            return await _locationRepository.GetAllLocationsAsync(cancellationToken);
        }
        public async Task<IEnumerable<Location>> GetAllLocationsNewAsync(CancellationToken cancellationToken = default)
        {
            return await _locationRepository.GetAllLocationsNewAsync(cancellationToken);
        }

        public async Task<LocationDetailsDto> GetLocationDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id, cancellationToken);

            if (location == null)
            {
                return null;
            }

            return MapLocationToDetailsDto(location);
        }

        private LocationDetailsDto MapLocationToDetailsDto(Location location)
        {
            return new LocationDetailsDto
            {
                Title = location.Title,
                SubTitle = location.SubTitle,
                Description = location.Description,
                Rooms = location.Rooms,
                NumberOfGuests = location.NumberOfGuests,
                PricePerDay = location.PricePerDay,
                Type = (int)location.Type,
                Features = (int)location.AvailableFeatures,
                Images = location.Images.Select(i => new ImageDto
                {
                    URL = i.Url,
                    IsCover = i.IsCover
                }).ToList(),
                Landlord = new LandlordDto
                {
                    Name = $"{location.Landlord.FirstName} {location.Landlord.LastName}",
                    Avatar = location.Landlord.Avatar?.Url
                }
            };
        }

        public async Task<MaxPriceDto> GetMaxPriceAsync(CancellationToken cancellationToken = default)
        {
            return await _locationRepository.GetMaxPriceAsync(cancellationToken);
            
        }
        public async Task<IEnumerable<DateTime>> GetUnAvailableDatesAsync(int locationId)
        {
            // Haal alle reserveringen voor de locatie
            var reservations = await _locationRepository.GetReservationsByLocationIdAsync(locationId);

            // Maak een lijst
            var unAvailableDates = new List<DateTime>();

            // Voeg de start tot en met einddatums toe aan de lijst
            foreach (var reservation in reservations)
            {
                var datesInRange = Enumerable.Range(0, (int)(reservation.EndDate - reservation.StartDate).TotalDays + 1)
                    .Select(offset => reservation.StartDate.AddDays(offset))
                    .ToList();

                unAvailableDates.AddRange(datesInRange);
            }

            return unAvailableDates;
        }
    }
}
