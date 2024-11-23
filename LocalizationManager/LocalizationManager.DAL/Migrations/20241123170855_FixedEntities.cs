using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalizationManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppUrl",
                table: "RegisteredApplications",
                newName: "AppId");

            migrationBuilder.AddColumn<string>(
                name: "AppId",
                table: "LocalizationValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppId",
                table: "LocalizationValues");

            migrationBuilder.RenameColumn(
                name: "AppId",
                table: "RegisteredApplications",
                newName: "AppUrl");
        }
    }
}
