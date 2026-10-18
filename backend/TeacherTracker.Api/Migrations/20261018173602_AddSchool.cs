using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeacherTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "teachers");

            migrationBuilder.RenameColumn(
                name: "OIB",
                table: "teachers",
                newName: "oib");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "teachers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "teachers",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "teachers",
                newName: "first_name");

            migrationBuilder.AddPrimaryKey(
                name: "pk_teachers",
                table: "teachers",
                column: "id");

            migrationBuilder.CreateTable(
                name: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    oib = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schools", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schools");

            migrationBuilder.DropPrimaryKey(
                name: "pk_teachers",
                table: "teachers");

            migrationBuilder.RenameTable(
                name: "teachers",
                newName: "Teachers");

            migrationBuilder.RenameColumn(
                name: "oib",
                table: "Teachers",
                newName: "OIB");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Teachers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Teachers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Teachers",
                newName: "FirstName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");
        }
    }
}
