using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addOnchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaNgay",
                table: "GiangDays",
                newName: "caNgay");

            migrationBuilder.AddColumn<bool>(
                name: "onSchedule",
                table: "GiangDays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "onSchedule",
                table: "GiangDays");

            migrationBuilder.RenameColumn(
                name: "caNgay",
                table: "GiangDays",
                newName: "CaNgay");
        }
    }
}
