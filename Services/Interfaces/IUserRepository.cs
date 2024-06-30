using WMSApp.Models.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WMSApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string userId);
        
        
        Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user);
        Task<bool> AddUserToRoleAsync(ApplicationUser user, string roleName);
        Task<bool> RemoveUserFromRoleAsync(ApplicationUser user, string roleName);
    }
}