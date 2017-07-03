using Microsoft.EntityFrameworkCore.Migrations;

namespace Cotal.App.Data.Migrations
{
  public partial class Initial2 : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<int>(
        "UserId",
        "Announcements",
        nullable: false,
        oldClrType: typeof(string),
        oldMaxLength: 128,
        oldNullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
        "UserId",
        "Announcements",
        maxLength: 128,
        nullable: true,
        oldClrType: typeof(int));
    }
  }
}