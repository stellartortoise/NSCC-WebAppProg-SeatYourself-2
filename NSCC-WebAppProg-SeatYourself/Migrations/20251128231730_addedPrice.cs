using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSCC_WebAppProg_SeatYourself.Migrations
{
    /// <inheritdoc />
    public partial class addedPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Occasion",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Occasion");
        }
    }
}
