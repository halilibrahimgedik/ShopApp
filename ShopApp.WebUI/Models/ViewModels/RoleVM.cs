using Microsoft.AspNetCore.Identity;
using ShopApp.WebUI.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class RoleVM
    {
        [Required]
        public  string Name { get; set; }
    }

    public class RoleDetails
    {
        public IdentityRole Role { get; set; }

        public IEnumerable<User> Members { get; set; }

        public IEnumerable<User> NonMembers { get; set; }
    }

    public class RoleEditModel
    {
        public RoleEditModel()
        {
            IdsToAdd = new string[] { };
            IdsToDelete = new string[] { };
        }
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public string[] IdsToAdd { get; set; }

        public string[] IdsToDelete { get; set; }
    }
}
