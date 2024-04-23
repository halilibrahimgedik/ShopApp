using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    OrderState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsHome = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "FORMAT(GETDATE(), 'yyyy-MM-dd')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Telefon kategorisi", "Telefon", "telefon" },
                    { 2, "Bilgisayar kategorisi", "Bilgisayar", "bilgisayar" },
                    { 3, "Elektronik aletler", "Elektronik", "elektronik" },
                    { 4, "Kitap Okumak", "Kitap", "kitap" },
                    { 5, "Elektronik ev aletleri", "Beyaz Eşya", "beyaz-esya" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "IsApproved", "IsHome", "Name", "Price", "Url" },
                values: new object[,]
                {
                    { 1, "çok iyi telefon", "iphone-green.jpg", true, true, "Iphone 15", 55000.0, "iphone15" },
                    { 2, "çok iyi telefon", "iphone-blue.jpg", true, true, "Iphone 15", 53000.0, "iphone15" },
                    { 3, "iyi telefon", "iphone-black.jpg", true, true, "Iphone 15", 52000.0, "iphone15" },
                    { 4, "çok iyi telefon", "iphone-pink.jpg", true, true, "Iphone 15", 51000.0, "iphone15" },
                    { 5, "çok iyi telefon", "iphone-yellow.jpg", true, true, "Iphone 15", 52000.0, "iphone15" },
                    { 6, "çok iyi Bilgisayar", "lenovo-office.jpg", true, true, "Lenovo Creative", 55000.0, "" },
                    { 7, "çok iyi Bilgisayar", "lenovo-gaming.jpg", true, true, "Lenovo Ideapad", 53000.0, "" },
                    { 8, "iyi Bilgisayar", "asus-office.jpg", true, false, "Asus Vivobook", 52000.0, "" },
                    { 9, "çok iyi Bilgisayar", "hp-office.jpg", true, true, "Hp latitude", 51000.0, "" },
                    { 10, "çok iyi Bilgisayar", "hp-gaming.jpg", true, true, "Hp Victus", 52000.0, "" },
                    { 11, "çok iyi Bilgisayar", "asus-gaming.jpg", true, true, "Asus Tuf", 52000.0, "" },
                    { 12, "Küçük değişimler ile neler yapabileceğinizi tahmin bile edemezsiniz", "atomik.jpg", true, true, "Atomik Alışkanlıklar", 320.0, "" },
                    { 13, "Başarılı insanlar hakkında anlatılan bir hikaye vardır; onların zeki ve hırslı oldukları söylenir. Outliers’te Malcolm Gladwell başarının gerçek hikayesinin bundan çok farklı olduğunu ve bazı insanların neden başarılı olduğunu anlamak için, bunların çevrelerine daha dikkatli bakmamız gerektiğini iddia ediyor.", "outliers.jpg", true, false, "Outliers (Çizginin Dışındakiler)", 340.0, "" },
                    { 14, "Bu kitap, ağ iletişiminin temel kavramlarından, İnternette sayısı her geçen gün artan güncel uygulamalara; farklı haberleşme teknolojilerinden ağ programlama tekniklerine kadar farklı yelpazedeki konuları gerek genel konseptleri gerekse teknik detayları ile açıklamaktadır.", "pc-network.jpg", true, false, "Bilgisayar Ağları ve İnternet", 360.0, "" },
                    { 15, "Alttan Donduruculu Buzdolabı", "bosch-buzdolap.jpg", true, true, "Bosch Buzdolabı", 20250.0, "" },
                    { 16, "Düşük enerjili yüksek performanslı", "arcelik-camasir.jpg", true, true, "Arçelik çamaşır Makinesi", 15750.0, "" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 3, 3 },
                    { 1, 4 },
                    { 3, 4 },
                    { 1, 5 },
                    { 3, 5 },
                    { 2, 6 },
                    { 3, 6 },
                    { 2, 7 },
                    { 3, 7 },
                    { 2, 8 },
                    { 3, 8 },
                    { 2, 9 },
                    { 3, 9 },
                    { 2, 10 },
                    { 3, 10 },
                    { 2, 11 },
                    { 3, 11 },
                    { 4, 12 },
                    { 4, 13 },
                    { 4, 14 },
                    { 3, 15 },
                    { 5, 15 },
                    { 3, 16 },
                    { 5, 16 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
