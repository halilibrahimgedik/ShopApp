﻿// <auto-generated />
using System;
using DataAccessLayer.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ShopAppContext))]
    partial class ShopAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("EntityLayer.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Telefon kategorisi",
                            Name = "Telefon"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Bilgisayar kategorisi",
                            Name = "Bilgisayar"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Elektronik aletler",
                            Name = "Elektronik"
                        });
                });

            modelBuilder.Entity("EntityLayer.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "çok iyi telefon",
                            ImageUrl = "/img/iphone-green.jpg",
                            IsApproved = true,
                            Name = "Iphone 15",
                            Price = 55000.0
                        },
                        new
                        {
                            Id = 2,
                            Description = "çok iyi telefon",
                            ImageUrl = "/img/iphone-blue.jpg",
                            IsApproved = true,
                            Name = "Iphone 15",
                            Price = 53000.0
                        },
                        new
                        {
                            Id = 3,
                            Description = "iyi telefon",
                            ImageUrl = "/img/iphone-black.jpg",
                            IsApproved = true,
                            Name = "Iphone 15",
                            Price = 52000.0
                        },
                        new
                        {
                            Id = 4,
                            Description = "çok iyi telefon",
                            ImageUrl = "/img/iphone-pink.jpg",
                            IsApproved = true,
                            Name = "Iphone 15",
                            Price = 51000.0
                        },
                        new
                        {
                            Id = 5,
                            Description = "çok iyi telefon",
                            ImageUrl = "/img/iphone-yellow.jpg",
                            IsApproved = true,
                            Name = "Iphone 15",
                            Price = 52000.0
                        },
                        new
                        {
                            Id = 6,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "lenovo-office.jpg",
                            IsApproved = true,
                            Name = "Bilgisayar",
                            Price = 55000.0
                        },
                        new
                        {
                            Id = 7,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "lenovo-gaming.jpg",
                            IsApproved = true,
                            Name = "Bilgisayar",
                            Price = 53000.0
                        },
                        new
                        {
                            Id = 8,
                            Description = "iyi Bilgisayar",
                            ImageUrl = "asus-office.jpg",
                            IsApproved = true,
                            Name = "Bilgisayar",
                            Price = 52000.0
                        },
                        new
                        {
                            Id = 9,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "hp-office.jpg",
                            IsApproved = true,
                            Name = "Bilgisayar",
                            Price = 51000.0
                        },
                        new
                        {
                            Id = 10,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "hp-gaming.jpg",
                            IsApproved = true,
                            Name = "Bilgisayar",
                            Price = 52000.0
                        },
                        new
                        {
                            Id = 11,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "asus-gaming.jpg",
                            IsApproved = true,
                            Name = "Bilgisayar",
                            Price = 52000.0
                        });
                });

            modelBuilder.Entity("EntityLayer.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategory");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 3
                        });
                });

            modelBuilder.Entity("EntityLayer.ProductCategory", b =>
                {
                    b.HasOne("EntityLayer.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Product", "Product")
                        .WithMany("Categories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EntityLayer.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("EntityLayer.Product", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
