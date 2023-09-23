using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class EditProductVM
    {
        [Required(ErrorMessage = "Name field required")]
        [MinLength(4, ErrorMessage = "Name field must be 4 characters at least")]
        [MaxLength(60, ErrorMessage = "Name field must be 60 characters at least")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name field required")]
        [Range(1, 10000)]
        public double? Price { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Image field required")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Category field required")]
        public int? CategoryId { get; set; }
    }
}
