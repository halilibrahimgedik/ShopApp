using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Product
    {
        public Product()
        {
            Categories = new List<ProductCategory>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public double? Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsApproved { get; set; }

        public bool IsHome { get; set; }

        public List<ProductCategory> Categories { get; set; }

    }
}
