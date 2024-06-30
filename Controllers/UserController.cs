using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMSApp.Models.Identity;
using WMSApp.Models.ViewModels;
using WMSApp.Repositories.Interfaces;

namespace WMSApp.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserRepository _userRepository;

    public UserController(UserManager<ApplicationUser> userManager, IUserRepository userRepository, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return View(users);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);

        ViewBag.UserRoles = roles;
        return View(user);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.ToList();

        var model = new EditUserViewModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            UserRoles = userRoles,
            AllRoles = allRoles
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        if (ModelState.IsValid)
        {
            var userToUpdate = await _userManager.FindByIdAsync(model.UserId);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            // Get current roles of the user
            var currentRoles = await _userManager.GetRolesAsync(userToUpdate);

            // Remove current roles not selected in the new roles list
            var rolesToRemove = currentRoles.Except(model.SelectedRoles).ToArray();
            await _userManager.RemoveFromRolesAsync(userToUpdate, rolesToRemove);

            // Add new roles not already assigned
            var rolesToAdd = model.SelectedRoles.Except(currentRoles).ToArray();
            await _userManager.AddToRolesAsync(userToUpdate, rolesToAdd);

            return RedirectToAction("Index");
        }

        // If model state is invalid, redisplay the form with validation errors
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.ToList();

        model.UserName = user.UserName;
        model.UserRoles = userRoles;
        model.AllRoles = allRoles;

        return View(model);
    }
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _userRepository.DeleteUserAsync(id);
        return RedirectToAction(nameof(Index));
    }
}