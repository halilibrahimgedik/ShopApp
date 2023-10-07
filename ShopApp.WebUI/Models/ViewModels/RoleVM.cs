using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class RoleVM
    {
        [Required]
        public  string Name { get; set; }
    }
}
