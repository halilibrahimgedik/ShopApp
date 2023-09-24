using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace ShopApp.WebUI.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["action"].ToString().ToLower() == "list")
            {
                ViewBag.SelectedCategory = RouteData?.Values["id"]; // ? null olup olmadığınıda kontrol ediyor
            }

            return View(_categoryRepository.GetAll());
        }
    }
}
