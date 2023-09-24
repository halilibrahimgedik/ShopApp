using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace ShopApp.WebUI.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["action"].ToString().ToLower() == "list")
            {
                ViewBag.SelectedCategory = RouteData?.Values["category"]; // ? null olup olmadığınıda kontrol ediyor
            }

            return View(_categoryService.GetAll());
        }
    }
}
