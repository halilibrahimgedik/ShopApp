using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Category Url", Prompt = "Enter a category Url should be unique")]
        public string Url { get; set; }

        public string Description { get; set; }
    }
}
