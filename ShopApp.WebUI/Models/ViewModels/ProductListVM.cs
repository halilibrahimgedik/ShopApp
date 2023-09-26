using EntityLayer;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class ProductListVM
    {
        public List<Product> Products { get; set; }

        public PageInfo PageInfo { get; set; }
    }

    public class PageInfo
    {
        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public string CurrentCategory { get; set; }

        // TODO 10 ürünümüz olsa her sayfada 3 ürün göstersek 10/3 den 3.3 gibi bir değer geliyor yani biz 
        // TODO 3.3 sayfa mı göstericez ?, hayır 4 sayfa gösterip 4. sayfada 1 tane ürün sergilenmesi lazım.
        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems/ItemsPerPage);
        }
    }
}
