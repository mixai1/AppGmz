using Microsoft.EntityFrameworkCore.Migrations;

namespace AppGmz.DAL.Migrations
{
    public partial class addurlpropertyinnewsRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "RecordNewses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "RecordNewses");
        }
    }
}
