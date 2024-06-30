using WMSApp.Models;

namespace WMSApp.Services.Interfaces;

public interface ILocationRepository
{
    Task<Location> GetByIdAsync(int id);
    Task<IEnumerable<Location>> GetAllAsync();
    Task<Location> AddAsync(Location location);
    Task<Location> UpdateAsync(Location location);
    Task DeleteAsync(int id);
}