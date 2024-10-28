using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DIYManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedbackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DIYEveningID",
                table: "DIYRegistrations",
                newName: "DIYEveningId");

            migrationBuilder.CreateTable(
                name: "DIYFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DIYEveningId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIYFeedback", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DIYFeedback");

            migrationBuilder.RenameColumn(
                name: "DIYEveningId",
                table: "DIYRegistrations",
                newName: "DIYEveningID");
        }
    }
}
