using Microsoft.EntityFrameworkCore.Migrations;

namespace aiproject.Migrations
{
    public partial class ticketrel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserEntityId",
                table: "Tickets",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserEntityId",
                table: "Tickets",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserEntityId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UserEntityId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tickets");
        }
    }
}
