using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addfullday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaNgay",
                table: "LichThucHanhs");

            migrationBuilder.AddColumn<bool>(
                name: "CaNgay",
                table: "GiangDays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaNgay",
                table: "GiangDays");

            migrationBuilder.AddColumn<bool>(
                name: "CaNgay",
                table: "LichThucHanhs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
