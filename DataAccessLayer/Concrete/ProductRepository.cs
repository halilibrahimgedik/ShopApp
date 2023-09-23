﻿using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public static class ProductRepository
    {
        private static List<Product> _products = null;

        static ProductRepository()
        {
            _products = new List<Product>()
            {
                new Product {Id=1,Name="Iphone 15",Price=55000,Description="çok iyi telefon", ImageUrl="iphone-green.jpg",IsApproved=true, CategoryId=1},
                new Product {Id=2,Name="Iphone 15",Price=53000,Description="çok iyi telefon", ImageUrl="iphone-blue.jpg",IsApproved=true,CategoryId=1},
                new Product {Id=3,Name="Iphone 15",Price=52000,Description="iyi telefon", ImageUrl="iphone-black.jpg",IsApproved=true, CategoryId=1},
                new Product {Id=4,Name="Iphone 15",Price=51000,Description="çok iyi telefon", ImageUrl="iphone-pink.jpg",IsApproved=true, CategoryId=1},
                new Product {Id=5,Name="Iphone 15",Price=52000,Description="çok iyi telefon", ImageUrl="iphone-yellow.jpg",IsApproved=true, CategoryId=1},

                new Product {Id=6,Name="Bilgisayar",Price=55000,Description="çok iyi Bilgisayar", ImageUrl="lenovo-office.jpg",IsApproved=true, CategoryId=2},
                new Product {Id=7,Name="Bilgisayar",Price=53000,Description="çok iyi Bilgisayar", ImageUrl="lenovo-gaming.jpg",IsApproved=true,CategoryId=2},
                new Product {Id=8,Name="Bilgisayar",Price=52000,Description="iyi Bilgisayar", ImageUrl="asus-office.jpg",IsApproved=true, CategoryId=2},
                new Product {Id=9,Name="Bilgisayar",Price=51000,Description="çok iyi Bilgisayar", ImageUrl="hp-office.jpg",IsApproved=true, CategoryId=2},
                new Product {Id=10,Name="Bilgisayar",Price=52000,Description="çok iyi Bilgisayar", ImageUrl="hp-gaming.jpg",IsApproved=true, CategoryId=2},
                new Product {Id=11,Name="Bilgisayar",Price=52000,Description="çok iyi Bilgisayar", ImageUrl="asus-gaming.jpg",IsApproved=true, CategoryId=2}

            };
        }

        public static List<Product> Products { get { return _products; } }


        public static void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public static Product GetProductById(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        public static void UpdateProduct(Product p)
        {
            foreach (var product in _products) 
            {
                if (product.Id == p.Id)
                {
                    product.Name = p.Name;
                    product.Price = p.Price;
                    product.Description = p.Description;
                    product.ImageUrl = p.ImageUrl;
                    product.CategoryId = p.CategoryId;
                }
            }
        }

        public static void DeleteProduct(int? id)
        {
            if (id != null)
            {
                var product = Products.Find(x => x.Id == id);
                Products.Remove(product);
            }
        }
    }
}