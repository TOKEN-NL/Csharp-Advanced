using Csharp_Advanced.Models;
using Csharp_Advanced.Repositories;


namespace Csharp_Advanced.Services
{
    public class LocationService
    {
        private readonly LocationRepository _locationRepository;

        public LocationService(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return _locationRepository.GetAllLocations();
        }
        public IEnumerable<Location> GetAllLocationsNew()
        {
            return _locationRepository.GetAllLocationsNew();
        }
    }
}
