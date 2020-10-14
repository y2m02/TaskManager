using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Migrations
{
    public partial class TasksrenamedtoAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Assignments_TaskId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "Assignments",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Assignments_TaskId",
                table: "Schedules",
                column: "TaskId",
                principalTable: "Assignments",
                principalColumn: "AssignmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Assignments_TaskId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Assignments_TaskId",
                table: "Schedules",
                column: "TaskId",
                principalTable: "Assignments",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
