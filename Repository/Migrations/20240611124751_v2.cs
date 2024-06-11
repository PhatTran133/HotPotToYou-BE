using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateByID",
                table: "UtensilDetai");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "UtensilDetai");

            migrationBuilder.DropColumn(
                name: "DeleteByID",
                table: "UtensilDetai");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "UtensilDetai");

            migrationBuilder.DropColumn(
                name: "UpdateByID",
                table: "UtensilDetai");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "UtensilDetai");

            migrationBuilder.DropColumn(
                name: "CreateByID",
                table: "OrderActivity");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "OrderActivity");

            migrationBuilder.DropColumn(
                name: "DeleteByID",
                table: "OrderActivity");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "OrderActivity");

            migrationBuilder.DropColumn(
                name: "UpdateByID",
                table: "OrderActivity");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "OrderActivity");

            migrationBuilder.DropColumn(
                name: "CreateByID",
                table: "HotPotUtensilType");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "HotPotUtensilType");

            migrationBuilder.DropColumn(
                name: "DeleteByID",
                table: "HotPotUtensilType");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "HotPotUtensilType");

            migrationBuilder.DropColumn(
                name: "UpdateByID",
                table: "HotPotUtensilType");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "HotPotUtensilType");

            migrationBuilder.DropColumn(
                name: "CreateByID",
                table: "HotPotIngredient");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "HotPotIngredient");

            migrationBuilder.DropColumn(
                name: "DeleteByID",
                table: "HotPotIngredient");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "HotPotIngredient");

            migrationBuilder.DropColumn(
                name: "UpdateByID",
                table: "HotPotIngredient");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "HotPotIngredient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateByID",
                table: "UtensilDetai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "UtensilDetai",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByID",
                table: "UtensilDetai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "UtensilDetai",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByID",
                table: "UtensilDetai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "UtensilDetai",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateByID",
                table: "OrderActivity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "OrderActivity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByID",
                table: "OrderActivity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "OrderActivity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByID",
                table: "OrderActivity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "OrderActivity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateByID",
                table: "HotPotUtensilType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "HotPotUtensilType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByID",
                table: "HotPotUtensilType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "HotPotUtensilType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByID",
                table: "HotPotUtensilType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "HotPotUtensilType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateByID",
                table: "HotPotIngredient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "HotPotIngredient",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByID",
                table: "HotPotIngredient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "HotPotIngredient",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByID",
                table: "HotPotIngredient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "HotPotIngredient",
                type: "datetime2",
                nullable: true);
        }
    }
}
