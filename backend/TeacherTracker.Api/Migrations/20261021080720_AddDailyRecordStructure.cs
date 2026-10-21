using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyRecordStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "daily_records",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    monthly_record_id = table.Column<int>(type: "integer", nullable: false),
                    work_date = table.Column<DateOnly>(type: "date", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    created_by_user_id = table.Column<string>(type: "text", nullable: false),
                    updated_by_user_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_daily_records", x => x.id);
                    table.ForeignKey(
                        name: "fk_daily_records_monthly_records_monthly_record_id",
                        column: x => x.monthly_record_id,
                        principalTable: "monthly_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "daily_attendance_slots",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    daily_record_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shift = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    slot_number = table.Column<int>(type: "integer", nullable: false),
                    attendance_code_id = table.Column<int>(type: "integer", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_daily_attendance_slots", x => x.id);
                    table.ForeignKey(
                        name: "fk_daily_attendance_slots_attendance_codes_attendance_code_id",
                        column: x => x.attendance_code_id,
                        principalTable: "attendance_codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_daily_attendance_slots_daily_records_daily_record_id",
                        column: x => x.daily_record_id,
                        principalTable: "daily_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "daily_work_entries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    daily_record_id = table.Column<Guid>(type: "uuid", nullable: false),
                    work_category_id = table.Column<int>(type: "integer", nullable: false),
                    hours = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_daily_work_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_daily_work_entries_daily_records_daily_record_id",
                        column: x => x.daily_record_id,
                        principalTable: "daily_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_daily_work_entries_work_categories_work_category_id",
                        column: x => x.work_category_id,
                        principalTable: "work_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_daily_attendance_slots_attendance_code_id",
                table: "daily_attendance_slots",
                column: "attendance_code_id");

            migrationBuilder.CreateIndex(
                name: "ix_daily_attendance_slots_daily_record_id_shift_slot_number",
                table: "daily_attendance_slots",
                columns: new[] { "daily_record_id", "shift", "slot_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_daily_records_monthly_record_id_work_date",
                table: "daily_records",
                columns: new[] { "monthly_record_id", "work_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_daily_work_entries_daily_record_id_work_category_id",
                table: "daily_work_entries",
                columns: new[] { "daily_record_id", "work_category_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_daily_work_entries_work_category_id",
                table: "daily_work_entries",
                column: "work_category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "daily_attendance_slots");

            migrationBuilder.DropTable(
                name: "daily_work_entries");

            migrationBuilder.DropTable(
                name: "daily_records");
        }
    }
}
