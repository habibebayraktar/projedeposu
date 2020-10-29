using Microsoft.EntityFrameworkCore.Migrations;

namespace Gelecek.Migrations
{
    public partial class sifreozellikdegisti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sifre",
                table: "TblUyeler",
                type: "varchar(45)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sifre",
                table: "TblUyeler",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)");
        }
    }
}
