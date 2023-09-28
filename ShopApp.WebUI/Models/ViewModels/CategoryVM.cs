using EntityLayer;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class CategoryVM
    {
        public CategoryVM()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Prompt = "Enter a category Url should be unique")]
        public string Url { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
