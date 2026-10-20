using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeacherTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkCodeCatalogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "work_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    employee_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    unit = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "attendance_codes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    category = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    employee_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    work_category_id = table.Column<int>(type: "integer", nullable: true),
                    hours_value = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendance_codes", x => x.id);
                    table.ForeignKey(
                        name: "fk_attendance_codes_work_categories_work_category_id",
                        column: x => x.work_category_id,
                        principalTable: "work_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_attendance_codes_code",
                table: "attendance_codes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_attendance_codes_work_category_id",
                table: "attendance_codes",
                column: "work_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_categories_code",
                table: "work_categories",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendance_codes");

            migrationBuilder.DropTable(
                name: "work_categories");
        }
    }
}
