using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateUtensil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderUtensil_OrderID",
                table: "OrderUtensil");

            migrationBuilder.DropIndex(
                name: "IX_HotPotPackage_OrderId",
                table: "HotPotPackage");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Utensil",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUtensil_OrderID",
                table: "OrderUtensil",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_HotPotPackage_OrderId",
                table: "HotPotPackage",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderUtensil_OrderID",
                table: "OrderUtensil");

            migrationBuilder.DropIndex(
                name: "IX_HotPotPackage_OrderId",
                table: "HotPotPackage");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Utensil");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUtensil_OrderID",
                table: "OrderUtensil",
                column: "OrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotPotPackage_OrderId",
                table: "HotPotPackage",
                column: "OrderId",
                unique: true);
        }
    }
}
