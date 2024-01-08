using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trello_app.Migrations
{
    /// <inheritdoc />
    public partial class fixpass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassSalt",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassSalt",
                table: "Users");
        }
    }
}
