using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class deleteunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HotPotIngredient_HotpotID",
                table: "HotPotIngredient");

            migrationBuilder.DropIndex(
                name: "IX_HotPotIngredient_IngredientId",
                table: "HotPotIngredient");

            migrationBuilder.DropIndex(
                name: "IX_HotPotPackage_HotPotID",
                table: "HotPotPackage");

            migrationBuilder.DropIndex(
                name: "IX_HotPotPackage_OrderId",
                table: "HotPotPackage");

            migrationBuilder.DropIndex(
                name: "IX_HotPotUtensilType_HotPotTypeID",
                table: "HotPotUtensilType");

            migrationBuilder.DropIndex(
                name: "IX_HotPotUtensilType_UtensilID",
                table: "HotPotUtensilType");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_OrderActivity_ActivityTypeID",
                table: "OrderActivity");

            migrationBuilder.DropIndex(
                name: "IX_OrderActivity_OrderID",
                table: "OrderActivity");

            migrationBuilder.DropIndex(
                name: "IX_OrderUtensil_OrderID",
                table: "OrderUtensil");

            migrationBuilder.DropIndex(
                name: "IX_OrderUtensil_UtensilID",
                table: "OrderUtensil");

            migrationBuilder.DropIndex(
                name: "IX_OrderUtensil_UtensilPackageID",
                table: "OrderUtensil");

            migrationBuilder.DropIndex(
                name: "IX_UtensilDetai_PackageID",
                table: "UtensilDetai");

            migrationBuilder.DropIndex(
                name: "IX_UtensilDetai_UtensilID",
                table: "UtensilDetai");

            // Recreate indexes without the unique constraint if needed
            migrationBuilder.CreateIndex(
                name: "IX_HotPotIngredient_HotpotID",
                table: "HotPotIngredient",
                column: "HotpotID");

            migrationBuilder.CreateIndex(
                name: "IX_HotPotIngredient_IngredientId",
                table: "HotPotIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_HotPotPackage_HotPotID",
                table: "HotPotPackage",
                column: "HotPotID");

            migrationBuilder.CreateIndex(
                name: "IX_HotPotPackage_OrderId",
                table: "HotPotPackage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_HotPotUtensilType_HotPotTypeID",
                table: "HotPotUtensilType",
                column: "HotPotTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_HotPotUtensilType_UtensilID",
                table: "HotPotUtensilType",
                column: "UtensilID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderActivity_ActivityTypeID",
                table: "OrderActivity",
                column: "ActivityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderActivity_OrderID",
                table: "OrderActivity",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUtensil_OrderID",
                table: "OrderUtensil",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUtensil_UtensilID",
                table: "OrderUtensil",
                column: "UtensilID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUtensil_UtensilPackageID",
                table: "OrderUtensil",
                column: "UtensilPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_UtensilDetai_PackageID",
                table: "UtensilDetai",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_UtensilDetai_UtensilID",
                table: "UtensilDetai",
                column: "UtensilID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recreate the unique indexes in the Down method if necessary
            migrationBuilder.CreateIndex(
                name: "IX_HotPotIngredient_HotpotID",
                table: "HotPotIngredient",
                column: "HotpotID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotPotIngredient_IngredientId",
                table: "HotPotIngredient",
                column: "IngredientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotPotPackage_HotPotID",
                table: "HotPotPackage",
                column: "HotPotID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotPotPackage_OrderId",
                table: "HotPotPackage",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotPotUtensilType_HotPotTypeID",
                table: "HotPotUtensilType",
                column: "HotPotTypeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotPotUtensilType_UtensilID",
                table: "HotPotUtensilType",
                column: "UtensilID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderActivity_ActivityTypeID",
                table: "OrderActivity",
                column: "ActivityTypeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderActivity_OrderID",
                table: "OrderActivity",
                column: "OrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderUtensil_OrderID",
                table: "OrderUtensil",
                column: "OrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderUtensil_UtensilID",
                table: "OrderUtensil",
                column: "UtensilID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderUtensil_UtensilPackageID",
                table: "OrderUtensil",
                column: "UtensilPackageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UtensilDetai_PackageID",
                table: "UtensilDetai",
                column: "PackageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UtensilDetai_UtensilID",
                table: "UtensilDetai",
                column: "UtensilID",
                unique: true);
        }
    }
}
