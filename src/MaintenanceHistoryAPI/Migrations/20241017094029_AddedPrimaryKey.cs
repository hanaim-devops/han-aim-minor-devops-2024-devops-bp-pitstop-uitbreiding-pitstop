using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pitstop.MaintenanceHistoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceHistory",
                table: "MaintenanceHistory");

            migrationBuilder.AlterColumn<string>(
                name: "LicenseNumber",
                table: "MaintenanceHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MaintenanceHistory",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceHistory",
                table: "MaintenanceHistory",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceHistory",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MaintenanceHistory");

            migrationBuilder.AlterColumn<string>(
                name: "LicenseNumber",
                table: "MaintenanceHistory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceHistory",
                table: "MaintenanceHistory",
                column: "LicenseNumber");
        }
    }
}
