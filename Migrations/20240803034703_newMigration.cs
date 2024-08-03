using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace final_project.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Departments_DepId",
                table: "Trainees");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Departments_DepId",
                table: "Trainees",
                column: "DepId",
                principalTable: "Departments",
                principalColumn: "DepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Departments_DepId",
                table: "Trainees");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Departments_DepId",
                table: "Trainees",
                column: "DepId",
                principalTable: "Departments",
                principalColumn: "DepId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
