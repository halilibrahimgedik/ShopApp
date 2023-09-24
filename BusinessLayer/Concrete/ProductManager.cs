﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public void Add(Product t)
        {
            _productRepository.Add(t);
        }

        public void Delete(Product t)
        {
            _productRepository.Delete(t);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        // ürünlerle birlikte kategorileride getirir
        public Product GetProductDetails(int id)
        {
            return _productRepository.GetProductDetails(id);
        }

        public List<Product> ListProductsByCategory(string name)
        {
            return _productRepository.ListProductsByCategory(name);
        }

        public void Update(Product t)
        {
            _productRepository.Update(t);
        }
    }
}
