using Microsoft.EntityFrameworkCore;
using WMSApp.DbContexts;
using WMSApp.Models;
using WMSApp.Services.Interfaces;

namespace WMSApp.Services;

public class LocationRepository : ILocationRepository
{
    private readonly WMSContext _context;

    public LocationRepository(WMSContext context)
    {
        _context = context;
    }
    public async Task<Location> GetByIdAsync(int id)
    {
        return await _context.Locations.FindAsync(id);
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location> AddAsync(Location location)
    {
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        return location;
    }

    public async Task<Location> UpdateAsync(Location location)
    {
        _context.Entry(location).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return location;
    }

    public async Task DeleteAsync(int id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location != null)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }
    }
}