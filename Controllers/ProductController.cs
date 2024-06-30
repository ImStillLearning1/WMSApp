using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMSApp.Models;
using WMSApp.Services.Interfaces;
using WMSApp.ViewModels;

namespace WMSApp.Controllers;

[Authorize(Roles = "Warehouseman, Warehouse manager, Admin")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            await _productRepository.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        var allCategories = await _categoryRepository.GetAllAsync();

        var model = new ProductViewModel
        {
            Product = product,
            AllCategories = allCategories
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductViewModel model)
    {
        var productToUpdate = await _productRepository.GetByIdAsync(model.Product.ProductId);
        model.Product.Category = await _categoryRepository.GetByIdAsync(model.Product.CategoryId);
        if (productToUpdate == null)
        {
            return NotFound();
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors);
        ModelState.Remove("Product.Category");
        var errors2 = ModelState.Values.SelectMany(v => v.Errors);
        TryValidateModel(model);
        var errors3 = ModelState.Values.SelectMany(v => v.Errors);
        if (ModelState.IsValid)
        {
            productToUpdate.Name = model.Product.Name;
            productToUpdate.Description = model.Product.Description;
            productToUpdate.CategoryId = model.Product.CategoryId;
            await _productRepository.UpdateAsync(productToUpdate);
            return RedirectToAction(nameof(Index));
        }
        var allCategories = await _categoryRepository.GetAllAsync();
        model.AllCategories = allCategories;
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}