using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.API.Migrations
{
    public partial class tms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Topics_TopicId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_OwnerId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Courses_CourseId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Topics_TopicId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_OwnerId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeedbacks_Courses_CourseId",
                table: "CourseFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeedbacks_Users_TraineeId",
                table: "CourseFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Users_UserId",
                table: "CourseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MOMs_Reviews_ReviewId",
                table: "MOMs");

            migrationBuilder.DropForeignKey(
                name: "FK_MOMs_Users_TraineeId",
                table: "MOMs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Departments_DepartmentId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewStatuses_StatusId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_TraineeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeFeedbacks_Courses_CourseId",
                table: "TraineeFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeFeedbacks_Users_TraineeId",
                table: "TraineeFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeFeedbacks_Users_TrainerId",
                table: "TraineeFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "CourseFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId" },
                keyValues: new object[] { 1, 12 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId" },
                keyValues: new object[] { 1, 13 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId" },
                keyValues: new object[] { 2, 16 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId" },
                keyValues: new object[] { 2, 17 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 1, 3, 8 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 1, 4, 12 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 1, 4, 13 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 1, 4, 14 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 2, 3, 7 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 2, 4, 16 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 2, 4, 17 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1861));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1862));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1863));

            migrationBuilder.UpdateData(
                table: "MOMs",
                keyColumns: new[] { "ReviewId", "TraineeId" },
                keyValues: new object[] { 1, 13 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "MOMs",
                keyColumns: new[] { "ReviewId", "TraineeId" },
                keyValues: new object[] { 4, 13 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1876));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1876));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1877));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1983), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1984) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1986), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1986) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1987), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1987) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1988), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1988) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1989), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1989) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1990), new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1990) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1755));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1767));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1768));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1769));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 9, 12, 14, 0, DateTimeKind.Local).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "TraineeFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId", "TrainerId" },
                keyValues: new object[] { 1, 12, 8 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "TraineeFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId", "TrainerId" },
                keyValues: new object[] { 1, 13, 8 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "TraineeFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId", "TrainerId" },
                keyValues: new object[] { 2, 16, 7 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "TraineeFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId", "TrainerId" },
                keyValues: new object[] { 2, 17, 7 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 13, 3, 42, 14, 0, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Topics_TopicId",
                table: "Assignments",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_OwnerId",
                table: "Assignments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Courses_CourseId",
                table: "Attendances",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Topics_TopicId",
                table: "Attendances",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_OwnerId",
                table: "Attendances",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeedbacks_Courses_CourseId",
                table: "CourseFeedbacks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeedbacks_Users_TraineeId",
                table: "CourseFeedbacks",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentId",
                table: "Courses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Users_UserId",
                table: "CourseUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MOMs_Reviews_ReviewId",
                table: "MOMs",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MOMs_Users_TraineeId",
                table: "MOMs",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Departments_DepartmentId",
                table: "Reviews",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewStatuses_StatusId",
                table: "Reviews",
                column: "StatusId",
                principalTable: "ReviewStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_TraineeId",
                table: "Reviews",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeFeedbacks_Courses_CourseId",
                table: "TraineeFeedbacks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeFeedbacks_Users_TraineeId",
                table: "TraineeFeedbacks",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeFeedbacks_Users_TrainerId",
                table: "TraineeFeedbacks",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Topics_TopicId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_OwnerId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Courses_CourseId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Topics_TopicId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_OwnerId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeedbacks_Courses_CourseId",
                table: "CourseFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeedbacks_Users_TraineeId",
                table: "CourseFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUsers_Users_UserId",
                table: "CourseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MOMs_Reviews_ReviewId",
                table: "MOMs");

            migrationBuilder.DropForeignKey(
                name: "FK_MOMs_Users_TraineeId",
                table: "MOMs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Departments_DepartmentId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewStatuses_StatusId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_TraineeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeFeedbacks_Courses_CourseId",
                table: "TraineeFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeFeedbacks_Users_TraineeId",
                table: "TraineeFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeFeedbacks_Users_TrainerId",
                table: "TraineeFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "CourseFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId" },
                keyValues: new object[] { 1, 12 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId" },
                keyValues: new object[] { 1, 13 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId" },
                keyValues: new object[] { 2, 16 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId" },
                keyValues: new object[] { 2, 17 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 1, 3, 8 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 1, 4, 12 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 1, 4, 13 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 1, 4, 14 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 2, 3, 7 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 2, 4, 16 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "CourseUsers",
                keyColumns: new[] { "CourseId", "RoleId", "UserId" },
                keyValues: new object[] { 2, 4, 17 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8675));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8680));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8683));

            migrationBuilder.UpdateData(
                table: "MOMs",
                keyColumns: new[] { "ReviewId", "TraineeId" },
                keyValues: new object[] { 1, 13 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "MOMs",
                keyColumns: new[] { "ReviewId", "TraineeId" },
                keyValues: new object[] { 4, 13 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8739));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8744));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8746));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9150), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9152) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9157), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9158) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9161), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9162) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9165), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9166) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9169), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9171) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "ReviewDate", "ReviewTime" },
                values: new object[] { new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9175), new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(9176) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8205));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8223));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8226));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8229));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 12, 22, 42, 605, DateTimeKind.Local).AddTicks(8234));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "TraineeFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId", "TrainerId" },
                keyValues: new object[] { 1, 12, 8 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "TraineeFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId", "TrainerId" },
                keyValues: new object[] { 1, 13, 8 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "TraineeFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId", "TrainerId" },
                keyValues: new object[] { 2, 16, 7 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "TraineeFeedbacks",
                keyColumns: new[] { "CourseId", "TraineeId", "TrainerId" },
                keyValues: new object[] { 2, 17, 7 },
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 2, 6, 52, 42, 605, DateTimeKind.Utc).AddTicks(8145));

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Topics_TopicId",
                table: "Assignments",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_OwnerId",
                table: "Assignments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Courses_CourseId",
                table: "Attendances",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Topics_TopicId",
                table: "Attendances",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_OwnerId",
                table: "Attendances",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeedbacks_Courses_CourseId",
                table: "CourseFeedbacks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeedbacks_Users_TraineeId",
                table: "CourseFeedbacks",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentId",
                table: "Courses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Users_UserId",
                table: "CourseUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MOMs_Reviews_ReviewId",
                table: "MOMs",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MOMs_Users_TraineeId",
                table: "MOMs",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Departments_DepartmentId",
                table: "Reviews",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewStatuses_StatusId",
                table: "Reviews",
                column: "StatusId",
                principalTable: "ReviewStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_TraineeId",
                table: "Reviews",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeFeedbacks_Courses_CourseId",
                table: "TraineeFeedbacks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeFeedbacks_Users_TraineeId",
                table: "TraineeFeedbacks",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeFeedbacks_Users_TrainerId",
                table: "TraineeFeedbacks",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
