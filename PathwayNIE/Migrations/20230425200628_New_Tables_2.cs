using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PathwayNIE.Migrations
{
    /// <inheritdoc />
    public partial class New_Tables_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_CVs_CvId",
                table: "UserLogins");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "CVs",
                newName: "LanguageSkills");

            migrationBuilder.AlterColumn<int>(
                name: "CvId",
                table: "UserLogins",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CharacteristicSetId",
                table: "UserLogins",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "CVs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contacts",
                table: "CVs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CoreSkills",
                table: "CVs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EducationInfo",
                table: "CVs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FCs",
                table: "CVs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsAchived = table.Column<bool>(type: "boolean", nullable: false),
                    CVId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_CVs_CVId",
                        column: x => x.CVId,
                        principalTable: "CVs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Characteristic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MotivationValue = table.Column<double>(type: "double precision", nullable: false),
                    IntelligenceValue = table.Column<double>(type: "double precision", nullable: false),
                    PsychologyProfile = table.Column<double>(type: "double precision", nullable: false),
                    Archetype = table.Column<double>(type: "double precision", nullable: false),
                    Engagement = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MakedTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserLoginId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MakedTasks_UserLogins_UserLoginId",
                        column: x => x.UserLoginId,
                        principalTable: "UserLogins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EmployerCardId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancies_EmployerCards_EmployerCardId",
                        column: x => x.EmployerCardId,
                        principalTable: "EmployerCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SolvedTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserIdId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Solution = table.Column<string>(type: "text", nullable: false),
                    CVId = table.Column<int>(type: "integer", nullable: true),
                    CharacteristicId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolvedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolvedTasks_CVs_CVId",
                        column: x => x.CVId,
                        principalTable: "CVs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SolvedTasks_Characteristic_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SolvedTasks_UserLogins_UserIdId",
                        column: x => x.UserIdId,
                        principalTable: "UserLogins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserIdId = table.Column<int>(type: "integer", nullable: false),
                    TaskIdId = table.Column<int>(type: "integer", nullable: false),
                    CharacteristicId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Characteristic_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Score_SolvedTasks_TaskIdId",
                        column: x => x.TaskIdId,
                        principalTable: "SolvedTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Score_UserLogins_UserIdId",
                        column: x => x.UserIdId,
                        principalTable: "UserLogins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    File = table.Column<byte[]>(type: "bytea", nullable: false),
                    MakedTaskId = table.Column<int>(type: "integer", nullable: true),
                    SolvedTaskId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAttachment_MakedTasks_MakedTaskId",
                        column: x => x.MakedTaskId,
                        principalTable: "MakedTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskAttachment_SolvedTasks_SolvedTaskId",
                        column: x => x.SolvedTaskId,
                        principalTable: "SolvedTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_CharacteristicSetId",
                table: "UserLogins",
                column: "CharacteristicSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_CVId",
                table: "Achievements",
                column: "CVId");

            migrationBuilder.CreateIndex(
                name: "IX_MakedTasks_UserLoginId",
                table: "MakedTasks",
                column: "UserLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_CharacteristicId",
                table: "Score",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_TaskIdId",
                table: "Score",
                column: "TaskIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_UserIdId",
                table: "Score",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_SolvedTasks_CharacteristicId",
                table: "SolvedTasks",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_SolvedTasks_CVId",
                table: "SolvedTasks",
                column: "CVId");

            migrationBuilder.CreateIndex(
                name: "IX_SolvedTasks_UserIdId",
                table: "SolvedTasks",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachment_MakedTaskId",
                table: "TaskAttachment",
                column: "MakedTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachment_SolvedTaskId",
                table: "TaskAttachment",
                column: "SolvedTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_EmployerCardId",
                table: "Vacancies",
                column: "EmployerCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_CVs_CvId",
                table: "UserLogins",
                column: "CvId",
                principalTable: "CVs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Characteristic_CharacteristicSetId",
                table: "UserLogins",
                column: "CharacteristicSetId",
                principalTable: "Characteristic",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_CVs_CvId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Characteristic_CharacteristicSetId",
                table: "UserLogins");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "TaskAttachment");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "MakedTasks");

            migrationBuilder.DropTable(
                name: "SolvedTasks");

            migrationBuilder.DropTable(
                name: "Characteristic");

            migrationBuilder.DropIndex(
                name: "IX_UserLogins_CharacteristicSetId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "CharacteristicSetId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "CVs");

            migrationBuilder.DropColumn(
                name: "Contacts",
                table: "CVs");

            migrationBuilder.DropColumn(
                name: "CoreSkills",
                table: "CVs");

            migrationBuilder.DropColumn(
                name: "EducationInfo",
                table: "CVs");

            migrationBuilder.DropColumn(
                name: "FCs",
                table: "CVs");

            migrationBuilder.RenameColumn(
                name: "LanguageSkills",
                table: "CVs",
                newName: "Info");

            migrationBuilder.AlterColumn<int>(
                name: "CvId",
                table: "UserLogins",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_CVs_CvId",
                table: "UserLogins",
                column: "CvId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
