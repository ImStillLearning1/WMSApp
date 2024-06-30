using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMSApp.Models;
using WMSApp.Services.Interfaces;

namespace WMSApp.Controllers;

[Authorize(Roles = "Warehouseman, Warehouse manager, Admin")]
public class InventoryController : Controller
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILocationRepository _locationRepository;

    public InventoryController(IInventoryRepository inventoryRepository, IProductRepository productRepository, ILocationRepository locationRepository)
    {
        _inventoryRepository = inventoryRepository;
        _productRepository = productRepository;
        _locationRepository = locationRepository;
    }

    public async Task<IActionResult> Index()
    {
        var inventories = await _inventoryRepository.GetAllAsync();
        return View(inventories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(id);
        if (inventory == null)
        {
            return NotFound();
        }
        return View(inventory);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Products = new SelectList(await _productRepository.GetAllAsync(), "ProductId", "Name");
        ViewBag.Locations = new SelectList(await _locationRepository.GetAllAsync(), "LocationId", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Inventory inventory)
    {
        if (ModelState.IsValid)
        {
            await _inventoryRepository.AddAsync(inventory);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Products = new SelectList(await _productRepository.GetAllAsync(), "ProductId", "Name");
        ViewBag.Locations = new SelectList(await _locationRepository.GetAllAsync(), "LocationId", "Name");
        return View(inventory);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(id);
        if (inventory == null)
        {
            return NotFound();
        }
        ViewBag.Products = new SelectList(await _productRepository.GetAllAsync(), "ProductId", "Name", inventory.ProductId);
        ViewBag.Locations = new SelectList(await _locationRepository.GetAllAsync(), "LocationId", "Name", inventory.LocationId);
        return View(inventory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Inventory inventory)
    {
        if (id != inventory.InventoryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _inventoryRepository.UpdateAsync(inventory);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Products = new SelectList(await _productRepository.GetAllAsync(), "ProductId", "Name", inventory.ProductId);
        ViewBag.Locations = new SelectList(await _locationRepository.GetAllAsync(), "LocationId", "Name", inventory.LocationId);
        return View(inventory);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(id);
        if (inventory == null)
        {
            return NotFound();
        }
        return View(inventory);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _inventoryRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}