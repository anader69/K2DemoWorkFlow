using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K2DemoWorkFlow.Infrastructure.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessRoleKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDecisionUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessActivities_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantStatusAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantStatusEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowInInbox = table.Column<bool>(type: "bit", nullable: false),
                    ShowInRequest = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: true),
                    ActivityTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskStatus_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCommentRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsCommentVisible = table.Column<bool>(type: "bit", nullable: false),
                    ProcessActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessActions_ProcessActivities_ProcessActivityId",
                        column: x => x.ProcessActivityId,
                        principalTable: "ProcessActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessActions_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessInstanceId = table.Column<int>(type: "int", nullable: true),
                    ProcessId = table.Column<int>(type: "int", nullable: true),
                    TaskStatusId = table.Column<int>(type: "int", nullable: true),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Originator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedToFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessActivityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_ProcessActivities_ProcessActivityId",
                        column: x => x.ProcessActivityId,
                        principalTable: "ProcessActivities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_TaskStatus_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalTable: "TaskStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessActionTrackings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessActionId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: true),
                    TaskAssignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskStatusId = table.Column<int>(type: "int", nullable: true),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    DelegatedFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DelegatedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessActionTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessActionTrackings_ProcessActions_ProcessActionId",
                        column: x => x.ProcessActionId,
                        principalTable: "ProcessActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessActionTrackings_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessActionTrackings_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessActionTrackings_TaskStatus_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalTable: "TaskStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActions_ProcessActivityId",
                table: "ProcessActions",
                column: "ProcessActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActions_ProcessId",
                table: "ProcessActions",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActionTrackings_ProcessActionId",
                table: "ProcessActionTrackings",
                column: "ProcessActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActionTrackings_ProcessId",
                table: "ProcessActionTrackings",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActionTrackings_TaskId",
                table: "ProcessActionTrackings",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActionTrackings_TaskStatusId",
                table: "ProcessActionTrackings",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActivities_ProcessId",
                table: "ProcessActivities",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProcessActivityId",
                table: "Tasks",
                column: "ProcessActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProcessId",
                table: "Tasks",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskStatusId",
                table: "Tasks",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatus_ProcessId",
                table: "TaskStatus",
                column: "ProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessActionTrackings");

            migrationBuilder.DropTable(
                name: "ProcessActions");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "ProcessActivities");

            migrationBuilder.DropTable(
                name: "TaskStatus");

            migrationBuilder.DropTable(
                name: "Processes");
        }
    }
}
