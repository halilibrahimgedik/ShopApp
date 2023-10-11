namespace ShopApp.WebUI.Models.ViewModels
{
    public class CartVM
    {
        public int CartId { get; set; }

        public List<CartItemVM> CartItems { get; set; }

        public double TotalPrice()
        {
            return (double)CartItems.Sum(i => i.Price * i.Quantity);
        }
    }

    public class CartItemVM
    {
        public int CartItemId { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }
    }
}
