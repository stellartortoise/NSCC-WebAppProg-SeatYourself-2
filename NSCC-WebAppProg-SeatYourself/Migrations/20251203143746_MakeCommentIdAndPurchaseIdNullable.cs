using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSCC_WebAppProg_SeatYourself.Migrations
{
    /// <inheritdoc />
    public partial class MakeCommentIdAndPurchaseIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Occasion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Occasion",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Occasion");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Occasion");
        }
    }
}
