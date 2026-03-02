using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopzyWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "OrderHeaders",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "OrderHeaders",
                newName: "Phone");
        }
    }
}
