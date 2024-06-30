using WMSApp.Models;

namespace WMSApp.Services.Interfaces;

public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(int? id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> AddAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task DeleteAsync(int id);
}