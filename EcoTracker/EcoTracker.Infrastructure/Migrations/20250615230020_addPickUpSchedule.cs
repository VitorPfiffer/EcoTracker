using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPickUpSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "WasteDisposals",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 15, 23, 0, 20, 91, DateTimeKind.Utc).AddTicks(5243),
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)",
                oldDefaultValue: new DateTime(2025, 6, 15, 22, 45, 5, 230, DateTimeKind.Utc).AddTicks(8004));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 15, 23, 0, 20, 89, DateTimeKind.Utc).AddTicks(5224),
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)",
                oldDefaultValue: new DateTime(2025, 6, 15, 22, 45, 5, 228, DateTimeKind.Utc).AddTicks(7932));

            migrationBuilder.CreateTable(
                name: "PickUpSchedules",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "RAW(900)", nullable: false),
                    Street = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Number = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Neighborhood = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    City = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    State = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PostalCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    WasteType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValue: new DateTime(2025, 6, 15, 23, 0, 20, 88, DateTimeKind.Utc).AddTicks(5477)),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IsDeleted = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickUpSchedules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickUpSchedules");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "WasteDisposals",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 15, 22, 45, 5, 230, DateTimeKind.Utc).AddTicks(8004),
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)",
                oldDefaultValue: new DateTime(2025, 6, 15, 23, 0, 20, 91, DateTimeKind.Utc).AddTicks(5243));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 15, 22, 45, 5, 228, DateTimeKind.Utc).AddTicks(7932),
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)",
                oldDefaultValue: new DateTime(2025, 6, 15, 23, 0, 20, 89, DateTimeKind.Utc).AddTicks(5224));
        }
    }
}
