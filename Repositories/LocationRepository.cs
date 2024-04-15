
using Csharp_Advanced.Models;
using Microsoft.EntityFrameworkCore;

namespace Csharp_Advanced.Repositories
{
    public class LocationRepository
    {
        private readonly AppDbContext _context;

        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return _context.Locations.ToList();
        }
        public IEnumerable<Location> GetAllLocationsNew()
        {
            return _context.Locations
                .Include(l => l.Images) 
                .Include(l => l.Landlord.Avatar) 
                .ToList();
        }
    }
}
