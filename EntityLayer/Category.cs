using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Category
    {
        public Category()
        {
            Products = new List<ProductCategory>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProductCategory> Products { get; set; }
    }
}
