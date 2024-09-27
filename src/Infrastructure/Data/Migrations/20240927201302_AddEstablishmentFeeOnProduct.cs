using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMeLoan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEstablishmentFeeOnProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstablishmentFee",
                table: "Products",
                type: "decimal(38,4)",
                precision: 38,
                scale: 4,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstablishmentFee",
                table: "Products");
        }
    }
}
