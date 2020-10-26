using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gelecek.Migrations
{
    public partial class createtbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblUyeler",
                columns: table => new
                {
                    Uyeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "varchar(50)", nullable: false),
                    Soyad = table.Column<string>(type: "varchar(75)", nullable: false),
                    Eposta = table.Column<string>(type: "varchar(75)", nullable: false),
                    Sifre = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUyeler", x => x.Uyeid);
                    table.UniqueConstraint("AK_TblUyeler_Eposta", x => x.Eposta);
                });

            migrationBuilder.CreateTable(
                name: "TblPostalar",
                columns: table => new
                {
                    Postaid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostaAdresi = table.Column<string>(type: "varchar(75)", nullable: false),
                    Uyeid = table.Column<int>(type: "int", nullable: false),
                    iletilecekZaman = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nezamanYazildi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    metin = table.Column<string>(type: "varchar(7000)", nullable: false),
                    metinKonusu = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPostalar", x => x.Postaid);
                    table.ForeignKey(
                        name: "FK_TblPostalar_TblUyeler_Uyeid",
                        column: x => x.Uyeid,
                        principalTable: "TblUyeler",
                        principalColumn: "Uyeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblPostalar_Uyeid",
                table: "TblPostalar",
                column: "Uyeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblPostalar");

            migrationBuilder.DropTable(
                name: "TblUyeler");
        }
    }
}
