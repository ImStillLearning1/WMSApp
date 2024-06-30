using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WMSApp.Controllers;

//[Authorize(Roles = "Admin")] // Restrict access to Admin role
[AllowAnonymous]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var roles = new List<string> { "Admin", "Warehouseman", "Warehouse manager" };
        return View(roles);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IdentityRole role)
    {
        if (ModelState.IsValid)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role.Name);
            if (!roleExists)
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Role already exists.");
            }
        }
        return View(role);
    }

    public async Task<IActionResult> Edit(string role)
    {
        var existingRole = await _roleManager.FindByNameAsync(role);
        if (existingRole == null)
        {
            return NotFound();
        }
        return View(existingRole);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(IdentityRole role)
    {
        var existingRole = await _roleManager.FindByIdAsync(role.Id);
        if (existingRole == null)
        {
            return NotFound();
        }

        existingRole.Name = role.Name;
        var result = await _roleManager.UpdateAsync(existingRole);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(role);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string role)
    {
        var existingRole = await _roleManager.FindByNameAsync(role);
        if (existingRole == null)
        {
            return NotFound();
        }

        var result = await _roleManager.DeleteAsync(existingRole);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(role);
    }
}