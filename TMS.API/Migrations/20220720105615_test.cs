using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.API.Migrations
{
    public partial class test : Migration
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

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Topics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5455));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5471));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5472));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5472));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5325));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5336));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5337));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5338));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 20, 16, 26, 15, 543, DateTimeKind.Local).AddTicks(5339));

            migrationBuilder.CreateIndex(
                name: "IX_MOMs_ReviewId",
                table: "MOMs",
                column: "ReviewId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Topics_TopicId",
                table: "Assignments",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_OwnerId",
                table: "Assignments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Courses_CourseId",
                table: "Attendances",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Topics_TopicId",
                table: "Attendances",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_OwnerId",
                table: "Attendances",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeedbacks_Courses_CourseId",
                table: "CourseFeedbacks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeedbacks_Users_TraineeId",
                table: "CourseFeedbacks",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentId",
                table: "Courses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Courses_CourseId",
                table: "CourseUsers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUsers_Users_UserId",
                table: "CourseUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MOMs_Reviews_ReviewId",
                table: "MOMs",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MOMs_Users_TraineeId",
                table: "MOMs",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewStatuses_StatusId",
                table: "Reviews",
                column: "StatusId",
                principalTable: "ReviewStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_TraineeId",
                table: "Reviews",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeFeedbacks_Courses_CourseId",
                table: "TraineeFeedbacks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeFeedbacks_Users_TraineeId",
                table: "TraineeFeedbacks",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeFeedbacks_Users_TrainerId",
                table: "TraineeFeedbacks",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.DropIndex(
                name: "IX_MOMs_ReviewId",
                table: "MOMs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Topics");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5335));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5337));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5337));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5348));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5349));

            migrationBuilder.UpdateData(
                table: "ReviewStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5231));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5232));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 6, 23, 10, 27, 17, 657, DateTimeKind.Local).AddTicks(5234));

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
