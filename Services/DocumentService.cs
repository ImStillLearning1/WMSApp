using Microsoft.EntityFrameworkCore;
using WMSApp.DbContexts;
using WMSApp.Services.Interfaces;
using WMSApp.Models;

namespace WMSApp.Services;

public class DocumentService : IDocumentService
{
    private readonly WMSContext _context;

    public DocumentService(WMSContext context)
    {
        _context = context;
    }

    public async Task<Document> GetDocumentByIdAsync(int id)
    {
        return _context.Documents
            .Include(d => d.Items)
            .ThenInclude(di => di.Product)
            .ThenInclude(dc => dc.Category)
            .FirstOrDefault(d => d.DocumentId == id);
    }

    public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
    {
        return await _context.Documents
            .Include(d => d.Items)
            .ThenInclude(di => di.Product)
            .ToListAsync();
    }

    public async Task<Document> CreateDocumentAsync(Document document)
    {
        _context.Documents.Add(document);
        await _context.SaveChangesAsync();
        return document;
    }

    public async Task<Document> UpdateDocumentAsync(Document document)
    {
        _context.Entry(document).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return document;
    }

    public async Task DeleteDocumentAsync(int id)
    {
        var document = await _context.Documents.FindAsync(id);
        if (document != null)
        {
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<DocumentItem> GetDocumentItemByIdAsync(int id)
    {
        return await _context.DocumentItems.FindAsync(id);
    }

    public async Task<IEnumerable<DocumentItem>> GetAllDocumentItemsAsync()
    {
        return await _context.DocumentItems.ToListAsync();
    }

    public async Task<DocumentItem> CreateDocumentItemAsync(DocumentItem item)
    {
        _context.DocumentItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<DocumentItem> UpdateDocumentItemAsync(DocumentItem item)
    {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task DeleteDocumentItemAsync(int id)
    {
        var item = await _context.DocumentItems.FindAsync(id);
        if (item != null)
        {
            _context.DocumentItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}

internal class ApplicationDbContext
{
}