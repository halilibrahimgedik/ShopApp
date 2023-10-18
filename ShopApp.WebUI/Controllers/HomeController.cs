using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.WebUI.Models;
using ShopApp.WebUI.Models.ViewModels;
using System.Diagnostics;
using System.Text.Json;

namespace ShopApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }



        public IActionResult Index()
        {
            var productListVM = new ListProductsVM()
            {
                Products = _productService.GetHomePageProducts()
            };

            return View(productListVM);
        }

        public async Task<IActionResult> GetProductsFromRestApi()
        {
            var products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync("https://localhost:7136/api/products"))
                {
                   string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }

            return View(products);
        }

        public IActionResult Details()
        {

            return View();
        }

        public IActionResult SearchProduct(string stringForSearch, int page =1)
        {
            const int pageSize = 3;

            var p = _productService.GetSearchResult(stringForSearch,page,pageSize);

            var searchListVm = new SearchListVM()
            {
                Products = p,
                SearchPageInfo = new SearchPageInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    StringForSearch=stringForSearch,
                    TotalItems = _productService.GetCountBySearchResult(stringForSearch)
                }
            };

            return View(searchListVm);
        }
    }
}