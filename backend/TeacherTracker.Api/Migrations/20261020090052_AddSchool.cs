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
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    oib = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schools", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_schools_oib",
                table: "schools",
                column: "oib",
                unique: true);
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
