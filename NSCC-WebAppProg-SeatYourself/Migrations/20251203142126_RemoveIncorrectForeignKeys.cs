using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSCC_WebAppProg_SeatYourself.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIncorrectForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Occasion");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Occasion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Occasion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Occasion",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
