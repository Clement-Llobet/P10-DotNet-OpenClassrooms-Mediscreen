using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mediscreen.Infrastructure.SqlServerDatabase.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientEntityWithNonRequiredAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 249,
                column: "BirthDate",
                value: new DateTime(2010, 8, 15, 10, 54, 55, 880, DateTimeKind.Local).AddTicks(392));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 294,
                column: "BirthDate",
                value: new DateTime(2020, 12, 8, 16, 19, 35, 866, DateTimeKind.Local).AddTicks(3368));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 339,
                column: "BirthDate",
                value: new DateTime(2013, 4, 2, 21, 44, 15, 852, DateTimeKind.Local).AddTicks(4281));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 772,
                column: "BirthDate",
                value: new DateTime(2006, 10, 12, 1, 37, 15, 873, DateTimeKind.Local).AddTicks(2803));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 816,
                column: "BirthDate",
                value: new DateTime(2017, 2, 4, 7, 1, 55, 859, DateTimeKind.Local).AddTicks(3838));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 249,
                column: "BirthDate",
                value: new DateTime(2010, 7, 11, 9, 47, 57, 378, DateTimeKind.Local).AddTicks(5170));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 294,
                column: "BirthDate",
                value: new DateTime(2020, 11, 3, 15, 12, 37, 364, DateTimeKind.Local).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 339,
                column: "BirthDate",
                value: new DateTime(2013, 2, 26, 20, 37, 17, 350, DateTimeKind.Local).AddTicks(9087));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 772,
                column: "BirthDate",
                value: new DateTime(2006, 9, 7, 0, 30, 17, 371, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 816,
                column: "BirthDate",
                value: new DateTime(2016, 12, 31, 5, 54, 57, 357, DateTimeKind.Local).AddTicks(8553));
        }
    }
}
