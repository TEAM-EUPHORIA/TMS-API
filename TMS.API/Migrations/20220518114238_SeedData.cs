using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.API.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDisabled = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MOMStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOMStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDisabled = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TraineeFeedbackStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeFeedbackStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDisabled = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "CourseStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDisabled = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDisabled = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CourseFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFeedbacks_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CourseFeedbacks_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CourseUser",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUser", x => new { x.CoursesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CourseUser_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CourseUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDisabled = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_ReviewStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ReviewStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TraineeFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDisabled = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraineeFeedbacks_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TraineeFeedbacks_TraineeFeedbackStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TraineeFeedbackStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TraineeFeedbacks_Users_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TraineeFeedbacks_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Document = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_AssignmentStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "AssignmentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Assignments_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Assignments_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_AttendanceStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "AttendanceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Attendances_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Attendances_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MOMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Agenda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeetingNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurposeOfMeeting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MOMs_MOMStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "MOMStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MOMs_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MOMs_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "AssignmentStatuses",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Submitted", null, null },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Yet to be Submitted", null, null }
                });

            migrationBuilder.InsertData(
                table: "AttendanceStatuses",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Present", null, null },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Absent", null, null }
                });

            migrationBuilder.InsertData(
                table: "CourseStatuses",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Completed", null, null },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "In Progress", null, null }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Name", "UpdatedBy", "UpdatedOn", "isDisabled" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), ".NET", null, null, false },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "JAVA", null, null, false },
                    { 3, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "LAMP", null, null, false }
                });

            migrationBuilder.InsertData(
                table: "MOMStatuses",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Submitted", null, null },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Yet to be Submitted", null, null }
                });

            migrationBuilder.InsertData(
                table: "ReviewStatuses",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Scheduled", null, null },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Upcoming", null, null },
                    { 3, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Canceled", null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Name", "UpdatedBy", "UpdatedOn", "isDisabled" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Training Head", null, null, false },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Co Ordinator", null, null, false },
                    { 3, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Trainer", null, null, false },
                    { 4, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Trainee", null, null, false },
                    { 5, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Reviewer", null, null, false }
                });

            migrationBuilder.InsertData(
                table: "TraineeFeedbackStatuses",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Given", null, null },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Yet to be Give", null, null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DepartmentId", "Description", "Duration", "Name", "StatusId", "UpdatedBy", "UpdatedOn", "isDisabled" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 3, "You can launch a new career in web development today by learning HTML & CSS. You don't need a computer science degree or expensive software. All you need is a computer, a bit of time, a lot of determination, and a teacher you trust. I've taught HTML and CSS to countless coworkers and held training sessions for fortune 100 companies. I am that teacher you can trust.", "11 hrs", "HTML 5 With CSS 3", null, null, null, false },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 3, "You can launch a new career by learning SQL RDBMS. You don't need a computer science degree or expensive software. All you need is a computer, a bit of time, a lot of determination, and a teacher you trust. I've taught RDBMS to countless coworkers and held training sessions for fortune 100 companies. I am that teacher you can trust.", "28 hrs", "SQL RDBMS", null, null, null, false }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Base64", "CreatedBy", "CreatedOn", "DepartmentId", "Email", "EmployeeId", "FullName", "Image", "Password", "RoleId", "UpdatedBy", "UpdatedOn", "UserName", "isDisabled" },
                values: new object[,]
                {
                    { 1, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), null, "Warren.Mackenzie@tms.edu.in", "TMS101", "Warren Mackenzie", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 1, null, null, "Warren", false },
                    { 2, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), null, "William.MacLeod@tms.edu.in", "TMS102", "William MacLeod", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 1, null, null, "William", false },
                    { 3, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), null, "Abigail.Manning@tms.edu.in", "TMS203", "Abigail Manning", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 2, null, null, "Abigail", false },
                    { 4, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), null, "Alexandra.Marshall@tms.edu.in", "TMS204", "Alexandra Marshall", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 2, null, null, "Alexandra", false },
                    { 5, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), null, "Alison.Martin@tms.edu.in", "TMS205", "Alison Martin", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 2, null, null, "Alison", false },
                    { 6, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 1, "Austin.Bailey@tms.edu.in", "TMS316", "Austin Bailey", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 3, null, null, "Austin", false },
                    { 7, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 2, "Benjamin.Baker@tms.edu.in", "TMS327", "Benjamin Baker", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 3, null, null, "Benjamin", false },
                    { 8, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 3, "Blake.Ball@tms.edu.in", "TMS338", "Blake Ball", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 3, null, null, "Blake", false },
                    { 9, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 1, "Ella.Payne@tms.edu.in", "TMS519", "Ella Payne", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 5, null, null, "Ella", false },
                    { 10, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 2, "Emily.Peake@tms.edu.in", "TMS5210", "Emily Peake", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 5, null, null, "Emily", false },
                    { 11, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 3, "Emma.Peters@tms.edu.in", "TMS5211", "Emma Peters", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 5, null, null, "Emma", false },
                    { 12, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 1, "Charles.Bower@tms.edu", "TMS4112", "Charles Bower", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 4, null, null, "Charles", false },
                    { 13, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 2, "Christian.Brown@tms.edu", "TMS4213", "Christian Brown", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 4, null, null, "Christian", false },
                    { 14, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 3, "Christopher.Buckland@tms.edu", "TMS4314", "Christopher Buckland", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 4, null, null, "Christopher", false },
                    { 15, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 1, "Gabrielle.Pullman@tms.edu.in", "TMS4115", "Gabrielle Pullman", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 4, null, null, "Gabrielle", false },
                    { 16, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 2, "Grace.Quinn@tms.edu.in", "TMS4216", "Grace Quinn", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 4, null, null, "Grace", false },
                    { 17, "data:image/jpeg;base64,", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), 3, "Hannah.Rampling@tms.edu.in", "TMS4217", "Hannah Rampling", null, "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=", 4, null, null, "Hannah", false }
                });

            migrationBuilder.InsertData(
                table: "CourseFeedbacks",
                columns: new[] { "Id", "CourseId", "CreatedBy", "CreatedOn", "Feedback", "OwnerId", "Rating", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", 13, 3.8f, null, null },
                    { 2, 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), " Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", 17, 4.3f, null, null },
                    { 3, 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", 12, 4.8f, null, null },
                    { 4, 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), " Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", 16, 3.6f, null, null }
                });

            migrationBuilder.InsertData(
                table: "CourseUser",
                columns: new[] { "CoursesId", "UsersId" },
                values: new object[,]
                {
                    { 1, 8 },
                    { 1, 12 },
                    { 1, 13 },
                    { 1, 14 },
                    { 2, 7 },
                    { 2, 16 },
                    { 2, 17 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Mode", "ReviewDate", "ReviewTime", "ReviewerId", "StatusId", "TraineeId", "UpdatedBy", "UpdatedOn", "isDisabled" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "online", "18-05-2022", "17:12:36.3571750", 11, 1, 13, null, null, false },
                    { 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Offline", "18-05-2022", "17:12:36.3571849", 11, 2, 15, null, null, false },
                    { 3, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "online", "18-05-2022", "17:12:36.3571902", 11, 3, 16, null, null, false },
                    { 4, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "online", "18-05-2022", "17:12:36.3571928", 10, 1, 12, null, null, false },
                    { 5, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Offline", "18-05-2022", "17:12:36.3571951", 9, 2, 12, null, null, false },
                    { 6, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "online", "18-05-2022", "17:12:36.3571973", 10, 3, 17, null, null, false }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Content", "CourseId", "CreatedBy", "CreatedOn", "Duration", "Name", "UpdatedBy", "UpdatedOn", "isDisabled" },
                values: new object[,]
                {
                    { 1, "All HTML documents must start with a document type declaration: <!DOCTYPE html>. \nThe HTML document itself begins with < html > and ends with </ html >.\nThe visible part of the HTML document is between<> and </ body >.\n HTML headings are defined with the <h1> to <h6> tags. \n< h1 > defines the most important heading. < h6 > defines the least important heading: \nHTML paragraphs are defined with the <p> tag \n HTML links are defined with the <a> tag \nHTML images are defined with the <img> tag.\nThe source file(src), alternative text(alt),width,and height are provided as attributes", 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "50 mins", "HTML Basics", null, null, false },
                    { 2, "All HTML documents must start with a document type declaration: <!DOCTYPE html>. \nThe HTML document itself begins with < html > and ends with </ html >.\nThe visible part of the HTML document is between<> and </ body >.\n HTML headings are defined with the <h1> to <h6> tags. \n< h1 > defines the most important heading. < h6 > defines the least important heading: \nHTML paragraphs are defined with the <p> tag \n HTML links are defined with the <a> tag \nHTML images are defined with the <img> tag.\nThe source file(src), alternative text(alt),width,and height are provided as attributes", 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "50 mins", "CSS Basics", null, null, false },
                    { 3, "All HTML documents must start with a document type declaration: <!DOCTYPE html>. \nThe HTML document itself begins with < html > and ends with </ html >.\nThe visible part of the HTML document is between<> and </ body >.\n HTML headings are defined with the <h1> to <h6> tags. \n< h1 > defines the most important heading. < h6 > defines the least important heading: \nHTML paragraphs are defined with the <p> tag \n HTML links are defined with the <a> tag \nHTML images are defined with the <img> tag.\nThe source file(src), alternative text(alt),width,and height are provided as attributes", 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "50 mins", "HTML & CSS Together", null, null, false },
                    { 4, "RDBMS stands for Relational DataBase Management Systems. It is basically a program that allows us to create, delete, and update a relational database. Relational Database is a database system that stores and retrieves data in a tabular format organized in the form of rows and columns.It is a smaller subset of DBMS which was designed by E.F Codd in the 1970s. The major DBMS like SQL, My-SQL, ORACLE are all based on the principles of relational DBMS", 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "50 mins", "RDBMS Basics", null, null, false },
                    { 5, "DBMS is the management of data that should remain integrated when any changes are done in it. It is because if the integrity of the data is affected, whole data will get disturbed and corrupted. Therefore, to maintain the integrity of the data, there are four properties described in the database management system, which are known as the ACID properties. The ACID properties are meant for the transaction that goes through a different group of tasks, and there we come to see the role of the ACID properties.\nIn this section, we will learn and understand about the ACID properties. We will learn what these properties stand for and what does each property is used for. We will also understand the ACID properties with the help of some examples.", 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "50 mins", "ACID Property", null, null, false }
                });

            migrationBuilder.InsertData(
                table: "TraineeFeedbacks",
                columns: new[] { "Id", "CourseId", "CreatedBy", "CreatedOn", "Feedback", "StatusId", "TraineeId", "TrainerId", "UpdatedBy", "UpdatedOn", "isDisabled" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", 1, 13, 8, null, null, false },
                    { 2, 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), " Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", 1, 17, 7, null, null, false },
                    { 3, 1, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", 1, 12, 8, null, null, false },
                    { 4, 2, null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), " Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", 1, 16, 7, null, null, false }
                });

            migrationBuilder.InsertData(
                table: "MOMs",
                columns: new[] { "Id", "Agenda", "CreatedBy", "CreatedOn", "MeetingNotes", "OwnerId", "PurposeOfMeeting", "ReviewId", "StatusId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, "Lorem ipsum dolor sit amet\nAdipiscing elit duis\nAenean euismod elementum", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Dolor purus non enim praesent elementum facilisis. Nisi scelerisque eu ultrices vitae auctor eu augue ut lectus. Adipiscing elit duis tristique sollicitudin nibh sit amet. Id eu nisl nunc mi ipsum faucibus vitae aliquet. Cras ornare arcu dui vivamus arcu felis bibendum ut tristique. Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat. Ut ornare lectus sit amet est placerat in egestas erat. Dictumst quisque sagittis purus sit amet volutpat consequat mauris nunc. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Ac auctor augue mauris augue. Sagittis orci a scelerisque purus semper eget. Massa placerat duis ultricies lacus sed turpis.", 13, "Eu consequat ac felis donec et odio pellentesque diam volutpat. Suspendisse in est ante in. Dis parturient montes nascetur ridiculus mus mauris vitae ultricies leo. Leo vel orci porta non pulvinar neque. Ultrices vitae auctor eu augue ut lectus arcu. Nec sagittis aliquam malesuada bibendum arcu vitae elementum curabitur vitae. Quisque sagittis purus sit amet volutpat. Semper feugiat nibh sed pulvinar proin gravida hendrerit lectus a.", 1, 1, null, null });

            migrationBuilder.InsertData(
                table: "MOMs",
                columns: new[] { "Id", "Agenda", "CreatedBy", "CreatedOn", "MeetingNotes", "OwnerId", "PurposeOfMeeting", "ReviewId", "StatusId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 2, "Lorem ipsum dolor sit amet\nAdipiscing elit duis\nAenean euismod elementum", null, new DateTime(2022, 5, 18, 11, 42, 36, 356, DateTimeKind.Utc).AddTicks(6686), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Dolor purus non enim praesent elementum facilisis. Nisi scelerisque eu ultrices vitae auctor eu augue ut lectus. Adipiscing elit duis tristique sollicitudin nibh sit amet. Id eu nisl nunc mi ipsum faucibus vitae aliquet. Cras ornare arcu dui vivamus arcu felis bibendum ut tristique. Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat. Ut ornare lectus sit amet est placerat in egestas erat. Dictumst quisque sagittis purus sit amet volutpat consequat mauris nunc. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Ac auctor augue mauris augue. Sagittis orci a scelerisque purus semper eget. Massa placerat duis ultricies lacus sed turpis.", 13, "Eu consequat ac felis donec et odio pellentesque diam volutpat. Suspendisse in est ante in. Dis parturient montes nascetur ridiculus mus mauris vitae ultricies leo. Leo vel orci porta non pulvinar neque. Ultrices vitae auctor eu augue ut lectus arcu. Nec sagittis aliquam malesuada bibendum arcu vitae elementum curabitur vitae. Quisque sagittis purus sit amet volutpat. Semper feugiat nibh sed pulvinar proin gravida hendrerit lectus a.", 4, 1, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_OwnerId",
                table: "Assignments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_StatusId",
                table: "Assignments",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TopicId",
                table: "Assignments",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_OwnerId",
                table: "Attendances",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StatusId",
                table: "Attendances",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_TopicId",
                table: "Attendances",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeedbacks_CourseId",
                table: "CourseFeedbacks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeedbacks_OwnerId",
                table: "CourseFeedbacks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StatusId",
                table: "Courses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUser_UsersId",
                table: "CourseUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_MOMs_OwnerId",
                table: "MOMs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MOMs_ReviewId",
                table: "MOMs",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_MOMs_StatusId",
                table: "MOMs",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StatusId",
                table: "Reviews",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TraineeId",
                table: "Reviews",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CourseId",
                table: "Topics",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeFeedbacks_CourseId",
                table: "TraineeFeedbacks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeFeedbacks_StatusId",
                table: "TraineeFeedbacks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeFeedbacks_TraineeId",
                table: "TraineeFeedbacks",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeFeedbacks_TrainerId",
                table: "TraineeFeedbacks",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "CourseFeedbacks");

            migrationBuilder.DropTable(
                name: "CourseUser");

            migrationBuilder.DropTable(
                name: "MOMs");

            migrationBuilder.DropTable(
                name: "TraineeFeedbacks");

            migrationBuilder.DropTable(
                name: "AssignmentStatuses");

            migrationBuilder.DropTable(
                name: "AttendanceStatuses");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "MOMStatuses");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TraineeFeedbackStatuses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "ReviewStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CourseStatuses");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}