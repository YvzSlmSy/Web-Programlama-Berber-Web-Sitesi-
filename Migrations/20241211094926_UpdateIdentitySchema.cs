using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BerberYonetimSistemi.Migrations
{
    public partial class UpdateIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Kullanicilar_KullaniciId",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_KullaniciId",
                table: "Calisanlar");

            migrationBuilder.AddColumn<string>(
                name: "KullaniciId1",
                table: "Randevular",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "Kullanicilar",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Kullanicilar",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Kullanicilar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Kullanicilar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Kullanicilar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Kullanicilar",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Kullanicilar",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Kullanicilar",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Kullanicilar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Kullanicilar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Kullanicilar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Kullanicilar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Kullanicilar",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RememberMe",
                table: "Kullanicilar",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Kullanicilar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Kullanicilar",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Kullanicilar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KullaniciId1",
                table: "Calisanlar",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId1",
                table: "Randevular",
                column: "KullaniciId1");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_KullaniciId1",
                table: "Calisanlar",
                column: "KullaniciId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Kullanicilar_KullaniciId1",
                table: "Calisanlar",
                column: "KullaniciId1",
                principalTable: "Kullanicilar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId1",
                table: "Randevular",
                column: "KullaniciId1",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Kullanicilar_KullaniciId1",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_KullaniciId1",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_KullaniciId1",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "KullaniciId1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "RememberMe",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "KullaniciId1",
                table: "Calisanlar");

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "Kullanicilar",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_KullaniciId",
                table: "Calisanlar",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Kullanicilar_KullaniciId",
                table: "Calisanlar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
