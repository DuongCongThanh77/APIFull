using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppApi.Migrations
{
    public partial class updateCategory4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryID",
                table: "Product",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryID",
                table: "Product",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
