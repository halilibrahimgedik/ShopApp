using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }


        public List<CartItem> CartItems { get; set; }

    }
}
