using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.TEST.Migrations
{
    /// <inheritdoc />
    public partial class AlterRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpStudentCourses",
                table: "AbpStudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_AbpStudentCourses_StudentId_CourseId",
                table: "AbpStudentCourses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpStudentCourses",
                table: "AbpStudentCourses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AbpStudentCourses_Id",
                table: "AbpStudentCourses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AbpStudentCourses_StudentId",
                table: "AbpStudentCourses",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpStudentCourses",
                table: "AbpStudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_AbpStudentCourses_Id",
                table: "AbpStudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_AbpStudentCourses_StudentId",
                table: "AbpStudentCourses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpStudentCourses",
                table: "AbpStudentCourses",
                columns: new[] { "StudentId", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpStudentCourses_StudentId_CourseId",
                table: "AbpStudentCourses",
                columns: new[] { "StudentId", "CourseId" });
        }
    }
}
