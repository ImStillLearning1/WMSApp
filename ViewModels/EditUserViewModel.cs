using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WMSApp.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
        public IList<IdentityRole> AllRoles { get; set; }
        public IList<string> SelectedRoles { get; set; }
    }
}