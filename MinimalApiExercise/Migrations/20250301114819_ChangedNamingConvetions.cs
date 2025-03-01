using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalApiExercise.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNamingConvetions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProductDescription",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "OrderProductId",
                table: "OrderProducts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerPhone",
                table: "Customers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "CustomerLastName",
                table: "Customers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "CustomerFirstName",
                table: "Customers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "CustomerEmail",
                table: "Customers",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "ProductPrice");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "ProductDescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderProducts",
                newName: "OrderProductId");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Customers",
                newName: "CustomerPhone");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customers",
                newName: "CustomerLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customers",
                newName: "CustomerFirstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customers",
                newName: "CustomerEmail");
        }
    }
}
