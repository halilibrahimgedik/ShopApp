using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace ShopApp.WebUI.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["action"].ToString().ToLower() == "list")
            {
                ViewBag.SelectedCategory = RouteData?.Values["id"]; // ? null olup olmadığınıda kontrol ediyor
            }
            
            return View(CategoryRepository.Categories);
        }
    }
}
