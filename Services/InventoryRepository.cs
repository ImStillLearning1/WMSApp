using Microsoft.EntityFrameworkCore;
using WMSApp.DbContexts;
using WMSApp.Models;
using WMSApp.Services.Interfaces;

namespace WMSApp.Services;

public class InventoryRepository : IInventoryRepository
{
    private readonly WMSContext _context;

    public InventoryRepository(WMSContext context)
    {
        _context = context;
    }

    public async Task<Inventory> GetByIdAsync(int id)
    {
        return await _context.Inventories
            .Include(i => i.Product)
            .Include(i => i.Location)
            .FirstOrDefaultAsync(i => i.InventoryId == id);
    }

    public async Task<IEnumerable<Inventory>> GetAllAsync()
    {
        return await _context.Inventories
            .Include(i => i.Product)
            .Include(i => i.Location)
            .ToListAsync();
    }

    public async Task<Inventory> AddAsync(Inventory inventory)
    {
        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();
        return inventory;
    }

    public async Task<Inventory> UpdateAsync(Inventory inventory)
    {
        _context.Entry(inventory).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return inventory;
    }

    public async Task DeleteAsync(int id)
    {
        var inventory = await _context.Inventories.FindAsync(id);
        if (inventory != null)
        {
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
        }
    }
}