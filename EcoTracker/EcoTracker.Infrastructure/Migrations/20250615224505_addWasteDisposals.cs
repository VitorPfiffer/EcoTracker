using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addWasteDisposals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 15, 22, 45, 5, 228, DateTimeKind.Utc).AddTicks(7932),
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)",
                oldDefaultValue: new DateTime(2025, 6, 15, 21, 40, 15, 843, DateTimeKind.Utc).AddTicks(4966));

            migrationBuilder.CreateTable(
                name: "WasteDisposals",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "RAW(900)", nullable: false),
                    WasteType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Unit = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UserId = table.Column<byte[]>(type: "RAW(900)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValue: new DateTime(2025, 6, 15, 22, 45, 5, 230, DateTimeKind.Utc).AddTicks(8004)),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IsDeleted = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteDisposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WasteDisposals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WasteDisposals_UserId",
                table: "WasteDisposals",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WasteDisposals");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 15, 21, 40, 15, 843, DateTimeKind.Utc).AddTicks(4966),
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)",
                oldDefaultValue: new DateTime(2025, 6, 15, 22, 45, 5, 228, DateTimeKind.Utc).AddTicks(7932));
        }
    }
}
