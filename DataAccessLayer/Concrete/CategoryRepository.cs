using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public static class CategoryRepository
    {
        private static List<Category> _categories;

        static CategoryRepository()
        {
            _categories = new List<Category>()
            {
                new(){Id=1, Name = "Telefonlar",Description = "Telefon Kategorisi"},
                new(){Id=2,Name = "Bilgisayar",Description = "Telefon Kategorisi"},
                new(){Id=3,Name = "Elektronik",Description = "Telefon Kategorisi"},
            };
        }

        public static List<Category> Categories { get { return _categories; } }

        public static void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public static Category GetCategoryById(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }
    }
}
