using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trello_app.Migrations
{
    /// <inheritdoc />
    public partial class deleteresponsibler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_Responsible_id",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_Responsible_id",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Responsible_id",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Responsible_id",
                table: "Notes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Responsible_id",
                table: "Notes",
                column: "Responsible_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_Responsible_id",
                table: "Notes",
                column: "Responsible_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
