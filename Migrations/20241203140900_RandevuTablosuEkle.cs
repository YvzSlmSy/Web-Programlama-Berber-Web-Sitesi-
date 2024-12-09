using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BerberYonetimSistemi.Migrations
{
    public partial class RandevuTablosuEkle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriAdi = table.Column<string>(type: "text", nullable: false),
                    MusteriTelefon = table.Column<string>(type: "text", nullable: false),
                    RandevuTarih = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CalisanId = table.Column<int>(type: "integer", nullable: false),
                    IslemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");
        }
    }
}
