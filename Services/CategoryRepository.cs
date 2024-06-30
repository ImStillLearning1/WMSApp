using Microsoft.EntityFrameworkCore;
using WMSApp.DbContexts;
using WMSApp.Models;
using WMSApp.Services.Interfaces;

namespace WMSApp.Services;

public class CategoryRepository : ICategoryRepository
{
    private readonly WMSContext _context;

    public CategoryRepository(WMSContext context)
    {
        _context = context;
    }
    
    public async Task<Category> GetByIdAsync(int? id)
    {
        return await _context.Categories.Include(i => i.Products).FirstOrDefaultAsync(x => x.CategoryId == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> AddAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}