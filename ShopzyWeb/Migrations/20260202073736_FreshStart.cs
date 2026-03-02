using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopzyWeb.Migrations
{
    /// <inheritdoc />
    public partial class FreshStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "OrderHeaders",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "OrderDetails",
                newName: "ProductTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "OrderHeaders",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ProductTitle",
                table: "OrderDetails",
                newName: "ProductName");
        }
    }
}
