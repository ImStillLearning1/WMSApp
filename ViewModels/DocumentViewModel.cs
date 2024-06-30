using WMSApp.Models;

namespace WMSApp.ViewModels;

public class DocumentViewModel
{
    public Document Document { get; set; }
    public IEnumerable<DocumentItem> DocumentItems { get; set; }
}