using Microsoft.EntityFrameworkCore.Migrations;

namespace Gelecek.Migrations
{
    public partial class Updatetwotbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblPostalar_TblUyeler_Uyeid",
                table: "TblPostalar");

            migrationBuilder.RenameColumn(
                name: "Uyeid",
                table: "TblPostalar",
                newName: "UyeId");

            migrationBuilder.RenameColumn(
                name: "Postaid",
                table: "TblPostalar",
                newName: "PostaId");

            migrationBuilder.RenameIndex(
                name: "IX_TblPostalar_Uyeid",
                table: "TblPostalar",
                newName: "IX_TblPostalar_UyeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPostalar_TblUyeler_UyeId",
                table: "TblPostalar",
                column: "UyeId",
                principalTable: "TblUyeler",
                principalColumn: "Uyeid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblPostalar_TblUyeler_UyeId",
                table: "TblPostalar");

            migrationBuilder.RenameColumn(
                name: "UyeId",
                table: "TblPostalar",
                newName: "Uyeid");

            migrationBuilder.RenameColumn(
                name: "PostaId",
                table: "TblPostalar",
                newName: "Postaid");

            migrationBuilder.RenameIndex(
                name: "IX_TblPostalar_UyeId",
                table: "TblPostalar",
                newName: "IX_TblPostalar_Uyeid");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPostalar_TblUyeler_Uyeid",
                table: "TblPostalar",
                column: "Uyeid",
                principalTable: "TblUyeler",
                principalColumn: "Uyeid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
