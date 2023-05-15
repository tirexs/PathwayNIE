using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PathwayNIE.Migrations
{
    /// <inheritdoc />
    public partial class NT_HZ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserLogins_UserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsEmployer",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsMentor",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsUser",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserLogins",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_RoleId",
                table: "UserLogins",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Roles_RoleId",
                table: "UserLogins",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Roles_RoleId",
                table: "UserLogins");

            migrationBuilder.DropIndex(
                name: "IX_UserLogins_RoleId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Roles");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployer",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMentor",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUser",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserLogins_UserId",
                table: "Roles",
                column: "UserId",
                principalTable: "UserLogins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
