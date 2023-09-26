using EntityLayer;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class ProductDetailVM
    {
        public Product Product { get; set; }

        public List<Category> Categories { get; set; }
    }
}
