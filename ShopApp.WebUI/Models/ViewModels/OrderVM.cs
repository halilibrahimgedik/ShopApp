namespace ShopApp.WebUI.Models.ViewModels
{
    public class OrderVM
    {
        public CartVM CartModel { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string? Note { get; set; }

        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationMonth { get; set; }

        public string ExpirationYear { get; set; }

        public string Cv { get; set; }
    }
}
