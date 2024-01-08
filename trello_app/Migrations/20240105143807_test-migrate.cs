using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trello_app.Migrations
{
    /// <inheritdoc />
    public partial class testmigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardSection_Board_Board_id",
                table: "BoardSection");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardUser_Board_BoardsId",
                table: "BoardUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_BoardSection_BoardSection_id",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Users_Responsible_id",
                table: "Note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                table: "Note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardSection",
                table: "BoardSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Board",
                table: "Board");

            migrationBuilder.RenameTable(
                name: "Note",
                newName: "Notes");

            migrationBuilder.RenameTable(
                name: "BoardSection",
                newName: "BoardSections");

            migrationBuilder.RenameTable(
                name: "Board",
                newName: "Boards");

            migrationBuilder.RenameIndex(
                name: "IX_Note_Responsible_id",
                table: "Notes",
                newName: "IX_Notes_Responsible_id");

            migrationBuilder.RenameIndex(
                name: "IX_Note_BoardSection_id",
                table: "Notes",
                newName: "IX_Notes_BoardSection_id");

            migrationBuilder.RenameIndex(
                name: "IX_BoardSection_Board_id",
                table: "BoardSections",
                newName: "IX_BoardSections_Board_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardSections",
                table: "BoardSections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boards",
                table: "Boards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardSections_Boards_Board_id",
                table: "BoardSections",
                column: "Board_id",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUser_Boards_BoardsId",
                table: "BoardUser",
                column: "BoardsId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_BoardSections_BoardSection_id",
                table: "Notes",
                column: "BoardSection_id",
                principalTable: "BoardSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_Responsible_id",
                table: "Notes",
                column: "Responsible_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardSections_Boards_Board_id",
                table: "BoardSections");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardUser_Boards_BoardsId",
                table: "BoardUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_BoardSections_BoardSection_id",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_Responsible_id",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardSections",
                table: "BoardSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boards",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "Note");

            migrationBuilder.RenameTable(
                name: "BoardSections",
                newName: "BoardSection");

            migrationBuilder.RenameTable(
                name: "Boards",
                newName: "Board");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_Responsible_id",
                table: "Note",
                newName: "IX_Note_Responsible_id");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_BoardSection_id",
                table: "Note",
                newName: "IX_Note_BoardSection_id");

            migrationBuilder.RenameIndex(
                name: "IX_BoardSections_Board_id",
                table: "BoardSection",
                newName: "IX_BoardSection_Board_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                table: "Note",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardSection",
                table: "BoardSection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Board",
                table: "Board",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardSection_Board_Board_id",
                table: "BoardSection",
                column: "Board_id",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUser_Board_BoardsId",
                table: "BoardUser",
                column: "BoardsId",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_BoardSection_BoardSection_id",
                table: "Note",
                column: "BoardSection_id",
                principalTable: "BoardSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Users_Responsible_id",
                table: "Note",
                column: "Responsible_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
