using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMSApp.Models;
using WMSApp.Services.Interfaces;

namespace WMSApp.Controllers;

[Authorize(Roles = "Warehouseman, Warehouse manager, Admin")]
public class LocationController : Controller
{
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<IActionResult> Index()
    {
        var locations = await _locationRepository.GetAllAsync();
        return View(locations);
    }

    public async Task<IActionResult> Details(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null)
        {
            return NotFound();
        }
        return View(location);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Location location)
    {
        if (ModelState.IsValid)
        {
            await _locationRepository.AddAsync(location);
            return RedirectToAction(nameof(Index));
        }
        return View(location);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null)
        {
            return NotFound();
        }
        return View(location);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Location location)
    {
        if (id != location.LocationId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _locationRepository.UpdateAsync(location);
            return RedirectToAction(nameof(Index));
        }
        return View(location);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null)
        {
            return NotFound();
        }
        return View(location);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _locationRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}