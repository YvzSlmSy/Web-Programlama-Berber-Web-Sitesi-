using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberYonetimSistemi.Migrations
{
    public partial class AddBerberIdToRandevular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BerberId",
                table: "Randevular",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_BerberId",
                table: "Randevular",
                column: "BerberId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanId",
                table: "Randevular",
                column: "CalisanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Berberler_BerberId",
                table: "Randevular",
                column: "BerberId",
                principalTable: "Berberler",
                principalColumn: "BerberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Berberler_BerberId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_BerberId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_CalisanId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "BerberId",
                table: "Randevular");
        }
    }
}
