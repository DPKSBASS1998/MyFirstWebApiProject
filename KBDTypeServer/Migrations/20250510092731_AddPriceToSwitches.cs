using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToSwitches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Switches",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Switches");
        }
    }
}
