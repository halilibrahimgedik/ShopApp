using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace ShopApp.WebUI.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var categories = new List<Category>()
            {
                new(){Name = "Telefonlar",Description = "Telefon Kategorisi"},
                new(){Name = "Bilgisayar",Description = "Telefon Kategorisi"},
                new(){Name = "Elektronik",Description = "Telefon Kategorisi"},
            };

            return View(categories);
        }
    }
}
