using WMSApp.Models;

namespace WMSApp.Services.Interfaces;

public interface IDocumentService
{
    Task<Document> GetDocumentByIdAsync(int id);
    Task<IEnumerable<Document>> GetAllDocumentsAsync();
    Task<Document> CreateDocumentAsync(Document document);
    Task<Document> UpdateDocumentAsync(Document document);
    Task DeleteDocumentAsync(int id);

    Task<DocumentItem> GetDocumentItemByIdAsync(int id);
    Task<IEnumerable<DocumentItem>> GetAllDocumentItemsAsync();
    Task<DocumentItem> CreateDocumentItemAsync(DocumentItem item);
    Task<DocumentItem> UpdateDocumentItemAsync(DocumentItem item);
    Task DeleteDocumentItemAsync(int id);
}