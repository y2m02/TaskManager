using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Migrations
{
    public partial class Tasksids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Assignments_TaskId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TaskId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_AssignmentId",
                table: "Schedules",
                column: "AssignmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Assignments_AssignmentId",
                table: "Schedules",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "AssignmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Assignments_AssignmentId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_AssignmentId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TaskId",
                table: "Schedules",
                column: "TaskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Assignments_TaskId",
                table: "Schedules",
                column: "TaskId",
                principalTable: "Assignments",
                principalColumn: "AssignmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
