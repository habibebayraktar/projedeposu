using Microsoft.EntityFrameworkCore.Migrations;

namespace Gelecek.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_TblUyeler_Eposta",
                table: "TblUyeler",
                column: "Eposta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_TblUyeler_Eposta",
                table: "TblUyeler");
        }
    }
}
