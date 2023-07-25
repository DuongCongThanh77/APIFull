using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppApi.Migrations
{
    public partial class updateCategory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID1",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryID1",
                table: "Product",
                column: "CategoryID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryID1",
                table: "Product",
                column: "CategoryID1",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryID1",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryID1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryID1",
                table: "Product");
        }
    }
}
