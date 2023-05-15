using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PathwayNIE.Migrations
{
    /// <inheritdoc />
    public partial class NewTables_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CvId",
                table: "UserLogins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployerCardId",
                table: "UserLogins",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Info = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Info = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerCards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_CvId",
                table: "UserLogins",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_EmployerCardId",
                table: "UserLogins",
                column: "EmployerCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_CVs_CvId",
                table: "UserLogins",
                column: "CvId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_EmployerCards_EmployerCardId",
                table: "UserLogins",
                column: "EmployerCardId",
                principalTable: "EmployerCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_CVs_CvId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_EmployerCards_EmployerCardId",
                table: "UserLogins");

            migrationBuilder.DropTable(
                name: "CVs");

            migrationBuilder.DropTable(
                name: "EmployerCards");

            migrationBuilder.DropIndex(
                name: "IX_UserLogins_CvId",
                table: "UserLogins");

            migrationBuilder.DropIndex(
                name: "IX_UserLogins_EmployerCardId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "CvId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "EmployerCardId",
                table: "UserLogins");
        }
    }
}
