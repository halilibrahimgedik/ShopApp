﻿using EntityLayer;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Url { get; set; }

        public double Price { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsApproved { get; set; }

        public bool IsHome { get; set; }

        public DateTime AddedDate { get; set; }

        public List<Category> SelectedCategories { get; set; } = new List<Category>();
    }
}
