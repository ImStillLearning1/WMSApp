using WMSApp.Models;

namespace WMSApp.Services.Interfaces;

public interface IInventoryRepository
{
    Task<Inventory> GetByIdAsync(int id);
    Task<IEnumerable<Inventory>> GetAllAsync();
    Task<Inventory> AddAsync(Inventory inventory);
    Task<Inventory> UpdateAsync(Inventory inventory);
    Task DeleteAsync(int id);
}