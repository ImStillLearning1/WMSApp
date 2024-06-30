using Microsoft.EntityFrameworkCore;
using WMSApp.DbContexts;
using WMSApp.Models;
using WMSApp.Services.Interfaces;

namespace WMSApp.Services;

public class ProductRepository : IProductRepository
{
    private readonly WMSContext _context;

    public ProductRepository(WMSContext context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(i => i.Category)
            .FirstOrDefaultAsync(i => i.ProductId == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(i => i.Category)
            .ToListAsync();
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}