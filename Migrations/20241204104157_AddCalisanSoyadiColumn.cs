using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberYonetimSistemi.Migrations
{
    public partial class AddCalisanSoyadiColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalisanSoyadi",
                table: "Calisanlar",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CalisanTelefon",
                table: "Calisanlar",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "BerberAdi",
                table: "Berberler",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalisanSoyadi",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "CalisanTelefon",
                table: "Calisanlar");

            migrationBuilder.AlterColumn<string>(
                name: "BerberAdi",
                table: "Berberler",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
