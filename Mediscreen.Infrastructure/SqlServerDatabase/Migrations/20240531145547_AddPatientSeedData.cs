using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mediscreen.Infrastructure.SqlServerDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "BirthDate", "FirstName", "Gender", "HomeAddress", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 249, new DateTime(2010, 7, 11, 9, 47, 57, 378, DateTimeKind.Local).AddTicks(5170), "Bernita", "F", "0239 Bradtke Port, Madelynnburgh, Nauru", "Konopelski", "1-701-937-1738 x8576" },
                    { 294, new DateTime(2020, 11, 3, 15, 12, 37, 364, DateTimeKind.Local).AddTicks(7944), "Max", "F", "58600 Heidenreich Isle, Casperhaven, Saint Martin", "Streich", "(340) 694-5534 x225" },
                    { 339, new DateTime(2013, 2, 26, 20, 37, 17, 350, DateTimeKind.Local).AddTicks(9087), "Edd", "M", "93004 Mertz Cove, Port Austenfort, Egypt", "Gislason", "(271) 872-7910 x731" },
                    { 772, new DateTime(2006, 9, 7, 0, 30, 17, 371, DateTimeKind.Local).AddTicks(7370), "Guillermo", "M", "700 Dane Branch, Georgianaburgh, Philippines", "Cummerata", "703-237-8869" },
                    { 816, new DateTime(2016, 12, 31, 5, 54, 57, 357, DateTimeKind.Local).AddTicks(8553), "Yoshiko", "M", "253 Jany Shoals, Oswaldomouth, Mali", "Maggio", "(838) 864-0031 x636" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 772);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 816);
        }
    }
}
