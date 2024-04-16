
using Csharp_Advanced.DataTransferObjects;
using Csharp_Advanced.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Csharp_Advanced.Repositories
{
    public class LocationRepository
    {
        private readonly AppDbContext _context;

        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Locations.ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<Location>> GetAllLocationsNewAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Locations
                .Include(l => l.Images) // Inclusief afbeeldingen
                .Include(l => l.Landlord.Avatar) // Inclusief landlord avatar
                .ToListAsync(cancellationToken);
        }
        public async Task<Location> GetLocationByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Locations
                .Include(l => l.Images)
                .Include(l => l.Landlord.Avatar)
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }

        public async Task<MaxPriceDto> GetMaxPriceAsync(CancellationToken cancellationToken)
        {
           float maxPrice = await _context.Locations.MaxAsync(l => l.PricePerDay, cancellationToken);
            return new MaxPriceDto { Price = (int)maxPrice };

        }
    }
}
