using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_model.Migrations
{
    public partial class AddCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizze_Category_CategoryId",
                table: "Pizze");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Pizze",
                newName: "CategoryId1");

            migrationBuilder.RenameIndex(
                name: "IX_Pizze_CategoryId",
                table: "Pizze",
                newName: "IX_Pizze_CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizze_Category_CategoryId1",
                table: "Pizze",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizze_Category_CategoryId1",
                table: "Pizze");

            migrationBuilder.RenameColumn(
                name: "CategoryId1",
                table: "Pizze",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Pizze_CategoryId1",
                table: "Pizze",
                newName: "IX_Pizze_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizze_Category_CategoryId",
                table: "Pizze",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
