using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conference.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CountryAndCityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Speakers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Speakers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Speakers");
        }
    }
}
