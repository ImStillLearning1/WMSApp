using WMSApp.Models;

namespace WMSApp.ViewModels;

public class ProductViewModel
{
    public Product Product { get; set; }
    public IEnumerable<Category>? AllCategories { get; set; }
}