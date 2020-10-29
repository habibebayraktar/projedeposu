using Microsoft.EntityFrameworkCore.Migrations;

namespace Gelecek.Migrations
{
    public partial class UpdateTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nezamanYazildi",
                table: "TblPostalar",
                newName: "NeZamanYazildi");

            migrationBuilder.RenameColumn(
                name: "metinKonusu",
                table: "TblPostalar",
                newName: "MetinKonusu");

            migrationBuilder.RenameColumn(
                name: "metin",
                table: "TblPostalar",
                newName: "Metin");

            migrationBuilder.RenameColumn(
                name: "iletilecekZaman",
                table: "TblPostalar",
                newName: "IletilecekZaman");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NeZamanYazildi",
                table: "TblPostalar",
                newName: "nezamanYazildi");

            migrationBuilder.RenameColumn(
                name: "MetinKonusu",
                table: "TblPostalar",
                newName: "metinKonusu");

            migrationBuilder.RenameColumn(
                name: "Metin",
                table: "TblPostalar",
                newName: "metin");

            migrationBuilder.RenameColumn(
                name: "IletilecekZaman",
                table: "TblPostalar",
                newName: "iletilecekZaman");
        }
    }
}
