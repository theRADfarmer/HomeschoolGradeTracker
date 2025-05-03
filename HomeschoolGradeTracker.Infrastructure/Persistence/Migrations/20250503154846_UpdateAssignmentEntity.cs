using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeschoolGradeTracker.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assignments");
        }
    }
}
