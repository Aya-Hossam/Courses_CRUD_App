using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace final_project.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepManager = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    minDegree = table.Column<int>(type: "int", nullable: false),
                    DepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepId",
                        column: x => x.DepId,
                        principalTable: "Departments",
                        principalColumn: "DepId");
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    DepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainees_Departments_DepId",
                        column: x => x.DepId,
                        principalTable: "Departments",
                        principalColumn: "DepId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InstAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstSalary = table.Column<int>(type: "int", nullable: false),
                    InstDepId = table.Column<int>(type: "int", nullable: false),
                    InstCrsId = table.Column<int>(type: "int", nullable: false),
                    CrsCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstId);
                    table.ForeignKey(
                        name: "FK_Instructors_Courses_CrsCourseId",
                        column: x => x.CrsCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_InstDepId",
                        column: x => x.InstDepId,
                        principalTable: "Departments",
                        principalColumn: "DepId");
                });

            migrationBuilder.CreateTable(
                name: "crsResults",
                columns: table => new
                {
                    ResId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResDegree = table.Column<int>(type: "int", nullable: false),
                    CrsId = table.Column<int>(type: "int", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crsResults", x => x.ResId);
                    table.ForeignKey(
                        name: "FK_crsResults_Courses_CrsId",
                        column: x => x.CrsId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_crsResults_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepId",
                table: "Courses",
                column: "DepId");

            migrationBuilder.CreateIndex(
                name: "IX_crsResults_CrsId",
                table: "crsResults",
                column: "CrsId");

            migrationBuilder.CreateIndex(
                name: "IX_crsResults_TraineeId",
                table: "crsResults",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_CrsCourseId",
                table: "Instructors",
                column: "CrsCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_InstDepId",
                table: "Instructors",
                column: "InstDepId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_DepId",
                table: "Trainees",
                column: "DepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crsResults");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
