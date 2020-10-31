using Microsoft.EntityFrameworkCore.Migrations;

namespace Gelecek.Migrations
{
    public partial class aktiflikeklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AktifMi",
                table: "TblUyeler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AktifMi",
                table: "TblUyeler");
        }
    }
}
