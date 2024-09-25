using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMeLoan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InterestFee",
                table: "Products",
                type: "decimal(38,4)",
                precision: 38,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimumMonthsTerm",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthsInterestFree",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestFee",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinimumMonthsTerm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MonthsInterestFree",
                table: "Products");
        }
    }
}
