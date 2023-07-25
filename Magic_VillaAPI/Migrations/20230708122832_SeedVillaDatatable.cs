using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaDatatable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 58, 32, 7, DateTimeKind.Local).AddTicks(7500));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 58, 32, 7, DateTimeKind.Local).AddTicks(7519));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 58, 32, 7, DateTimeKind.Local).AddTicks(7524));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 58, 32, 7, DateTimeKind.Local).AddTicks(7528));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 58, 32, 7, DateTimeKind.Local).AddTicks(7532));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 54, 2, 553, DateTimeKind.Local).AddTicks(8681));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 54, 2, 553, DateTimeKind.Local).AddTicks(8787));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 54, 2, 553, DateTimeKind.Local).AddTicks(8791));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 54, 2, 553, DateTimeKind.Local).AddTicks(8794));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 8, 17, 54, 2, 553, DateTimeKind.Local).AddTicks(8797));
        }
    }
}
