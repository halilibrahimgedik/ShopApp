﻿// <auto-generated />
using System;
using DataAccessLayer.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ShopAppContext))]
    [Migration("20231011055100_AddedCartEntity")]
    partial class AddedCartEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("EntityLayer.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("EntityLayer.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CartId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

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

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Telefon kategorisi",
                            Name = "Telefon",
                            Url = "telefon"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Bilgisayar kategorisi",
                            Name = "Bilgisayar",
                            Url = "bilgisayar"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Elektronik aletler",
                            Name = "Elektronik",
                            Url = "elektronik"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Kitap Okumak",
                            Name = "Kitap",
                            Url = "kitap"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Elektronik ev aletleri",
                            Name = "Beyaz Eşya",
                            Url = "beyaz-esya"
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

                    b.Property<bool>("IsHome")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "çok iyi telefon",
                            ImageUrl = "iphone-green.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Iphone 15",
                            Price = 55000.0,
                            Url = "iphone15"
                        },
                        new
                        {
                            Id = 2,
                            Description = "çok iyi telefon",
                            ImageUrl = "iphone-blue.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Iphone 15",
                            Price = 53000.0,
                            Url = "iphone15"
                        },
                        new
                        {
                            Id = 3,
                            Description = "iyi telefon",
                            ImageUrl = "iphone-black.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Iphone 15",
                            Price = 52000.0,
                            Url = "iphone15"
                        },
                        new
                        {
                            Id = 4,
                            Description = "çok iyi telefon",
                            ImageUrl = "iphone-pink.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Iphone 15",
                            Price = 51000.0,
                            Url = "iphone15"
                        },
                        new
                        {
                            Id = 5,
                            Description = "çok iyi telefon",
                            ImageUrl = "iphone-yellow.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Iphone 15",
                            Price = 52000.0,
                            Url = "iphone15"
                        },
                        new
                        {
                            Id = 6,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "lenovo-office.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Lenovo Creative",
                            Price = 55000.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 7,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "lenovo-gaming.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Lenovo Ideapad",
                            Price = 53000.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 8,
                            Description = "iyi Bilgisayar",
                            ImageUrl = "asus-office.jpg",
                            IsApproved = true,
                            IsHome = false,
                            Name = "Asus Vivobook",
                            Price = 52000.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 9,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "hp-office.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Hp latitude",
                            Price = 51000.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 10,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "hp-gaming.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Hp Victus",
                            Price = 52000.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 11,
                            Description = "çok iyi Bilgisayar",
                            ImageUrl = "asus-gaming.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Asus Tuf",
                            Price = 52000.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 12,
                            Description = "Küçük değişimler ile neler yapabileceğinizi tahmin bile edemezsiniz",
                            ImageUrl = "atomik.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Atomik Alışkanlıklar",
                            Price = 320.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 13,
                            Description = "Başarılı insanlar hakkında anlatılan bir hikaye vardır; onların zeki ve hırslı oldukları söylenir. Outliers’te Malcolm Gladwell başarının gerçek hikayesinin bundan çok farklı olduğunu ve bazı insanların neden başarılı olduğunu anlamak için, bunların çevrelerine daha dikkatli bakmamız gerektiğini iddia ediyor.",
                            ImageUrl = "outliers.jpg",
                            IsApproved = true,
                            IsHome = false,
                            Name = "Outliers (Çizginin Dışındakiler)",
                            Price = 340.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 14,
                            Description = "Bu kitap, ağ iletişiminin temel kavramlarından, İnternette sayısı her geçen gün artan güncel uygulamalara; farklı haberleşme teknolojilerinden ağ programlama tekniklerine kadar farklı yelpazedeki konuları gerek genel konseptleri gerekse teknik detayları ile açıklamaktadır.",
                            ImageUrl = "pc-network.jpg",
                            IsApproved = true,
                            IsHome = false,
                            Name = "Bilgisayar Ağları ve İnternet",
                            Price = 360.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 15,
                            Description = "Alttan Donduruculu Buzdolabı",
                            ImageUrl = "bosch-buzdolap.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Bosch Buzdolabı",
                            Price = 20250.0,
                            Url = ""
                        },
                        new
                        {
                            Id = 16,
                            Description = "Düşük enerjili yüksek performanslı",
                            ImageUrl = "arcelik-camasir.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Arçelik çamaşır Makinesi",
                            Price = 15750.0,
                            Url = ""
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
                            ProductId = 11,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 11,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 4
                        },
                        new
                        {
                            ProductId = 13,
                            CategoryId = 4
                        },
                        new
                        {
                            ProductId = 14,
                            CategoryId = 4
                        },
                        new
                        {
                            ProductId = 15,
                            CategoryId = 5
                        },
                        new
                        {
                            ProductId = 15,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 16,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 16,
                            CategoryId = 5
                        });
                });

            modelBuilder.Entity("EntityLayer.CartItem", b =>
                {
                    b.HasOne("EntityLayer.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
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

            modelBuilder.Entity("EntityLayer.Cart", b =>
                {
                    b.Navigation("CartItems");
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
