using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMSApp.Models;
using WMSApp.Services.Interfaces;
using WMSApp.ViewModels;

// Replace with your actual namespace

namespace WMSApp.Controllers;

[Authorize(Roles = "Warehouseman, Warehouse manager, Admin")]
public class DocumentController : Controller
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task<IActionResult> Index()
    {
        var documents = await _documentService.GetAllDocumentsAsync();
        return View(documents);
    }

    public async Task<IActionResult> Details(int id)
    {
        var document = await _documentService.GetDocumentByIdAsync(id);
        if (document == null)
        {
            return NotFound();
        }

        var viewModel = new DocumentViewModel
        {
            Document = document,
            DocumentItems = document.Items
        };

        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Document document)
    {
        if (ModelState.IsValid)
        {
            await _documentService.CreateDocumentAsync(document);
            return RedirectToAction(nameof(Index));
        }
        return View(document);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var document = await _documentService.GetDocumentByIdAsync(id);
        if (document == null)
        {
            return NotFound();
        }
        return View(document);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Document document)
    {
        if (id != document.DocumentId)
        {
            return NotFound();
        }
        
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        if (ModelState.IsValid)
        {
            await _documentService.UpdateDocumentAsync(document);
            return RedirectToAction(nameof(Index));
        }
        return View(document);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var document = await _documentService.GetDocumentByIdAsync(id);
        if (document == null)
        {
            return NotFound();
        }
        return View(document);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _documentService.DeleteDocumentAsync(id);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddItem(DocumentItem newItem)
    {
        var document = await _documentService.GetDocumentByIdAsync(newItem.DocumentId);
        if (document == null || document.Status == DocumentStatus.Completed)
        {
            return BadRequest("Cannot add items to a completed document.");
        }

        await _documentService.CreateDocumentItemAsync(newItem);
        return RedirectToAction("Details", new { id = newItem.DocumentId });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateItem(DocumentItem updatedItem)
    {
        await _documentService.UpdateDocumentItemAsync(updatedItem);
        return RedirectToAction("Details", new { id = updatedItem.DocumentId });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteItem(int itemId, int documentId)
    {
        await _documentService.DeleteDocumentItemAsync(itemId);
        return RedirectToAction("Details", new { id = documentId });
    }
}