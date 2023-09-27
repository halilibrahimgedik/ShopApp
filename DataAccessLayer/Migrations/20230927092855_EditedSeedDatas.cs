using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class EditedSeedDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "atomik.jpg", "Atomik Alışkanlıklar" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Başarılı insanlar hakkında anlatılan bir hikaye vardır; onların zeki ve hırslı oldukları söylenir. Outliers’te Malcolm Gladwell başarının gerçek hikayesinin bundan çok farklı olduğunu ve bazı insanların neden başarılı olduğunu anlamak için, bunların çevrelerine daha dikkatli bakmamız gerektiğini iddia ediyor.", "outliers.jpg", "Outliers (Çizginin Dışındakiler)" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Bu kitap, ağ iletişiminin temel kavramlarından, İnternette sayısı her geçen gün artan güncel uygulamalara; farklı haberleşme teknolojilerinden ağ programlama tekniklerine kadar farklı yelpazedeki konuları gerek genel konseptleri gerekse teknik detayları ile açıklamaktadır.", "pc-network.jpg", "Bilgisayar Ağları ve İnternet" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrl",
                value: "bosch-buzdolap.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "arcelik-camasir.jpg", "Arçelik çamaşır Makinesi" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "asus-gaming.jpg", "Atomik Alıkanlıklar" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Küçük değişimler ile neler yapabileceğinizi tahmin bile edemezsiniz", "asus-gaming.jpg", "Atomik Alıkanlıklar" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Küçük değişimler ile neler yapabileceğinizi tahmin bile edemezsiniz", "asus-gaming.jpg", "Atomik Alıkanlıklar" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrl",
                value: "asus-gaming.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "asus-gaming.jpg", "Arçelik Bulaşık Makinesi" });
        }
    }
}
