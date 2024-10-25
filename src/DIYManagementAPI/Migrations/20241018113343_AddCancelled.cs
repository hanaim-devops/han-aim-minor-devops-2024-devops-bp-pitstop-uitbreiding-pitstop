using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DIYManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCancelled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DIYEveningModels",
                table: "DIYEveningModels");

            migrationBuilder.RenameTable(
                name: "DIYEveningModels",
                newName: "DIYEvening");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "DIYEvening",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DIYEvening",
                table: "DIYEvening",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DIYEvening",
                table: "DIYEvening");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "DIYEvening");

            migrationBuilder.RenameTable(
                name: "DIYEvening",
                newName: "DIYEveningModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DIYEveningModels",
                table: "DIYEveningModels",
                column: "Id");
        }
    }
}
