using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.API.Migrations
{
    public partial class AddedIsDiabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_TraineeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_traineeFeedbacks_Users_TraineeId",
                table: "traineeFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_traineeFeedbacks_Users_TrainerId",
                table: "traineeFeedbacks");

            migrationBuilder.AddColumn<bool>(
                name: "isDisabled",
                table: "Topics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDisabled",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDisabled",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDisabled",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDisabled",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                name: "FK_traineeFeedbacks_Users_TraineeId",
                table: "traineeFeedbacks",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_traineeFeedbacks_Users_TrainerId",
                table: "traineeFeedbacks",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_TraineeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_traineeFeedbacks_Users_TraineeId",
                table: "traineeFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_traineeFeedbacks_Users_TrainerId",
                table: "traineeFeedbacks");

            migrationBuilder.DropColumn(
                name: "isDisabled",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "isDisabled",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isDisabled",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "isDisabled",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "isDisabled",
                table: "Courses");

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
                name: "FK_traineeFeedbacks_Users_TraineeId",
                table: "traineeFeedbacks",
                column: "TraineeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_traineeFeedbacks_Users_TrainerId",
                table: "traineeFeedbacks",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
