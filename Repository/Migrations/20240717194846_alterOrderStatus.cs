using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class alterOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Order",
                newName: "OrderStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "Order",
                newName: "PaymentStatus");
        }
    }
}
