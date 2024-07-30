using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiClases_20270722_Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class transaccionesañadidas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transacciones",
                columns: new[] { "Id", "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[,]
                {
                    { 1, 50.5, 30.199999999999999, new DateTime(2022, 3, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 2, 70.299999999999997, 45.799999999999997, new DateTime(2023, 8, 20, 15, 10, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 3, 25.100000000000001, 15.699999999999999, new DateTime(2024, 5, 5, 16, 20, 0, 0, DateTimeKind.Unspecified), 3, 4 },
                    { 4, 40.899999999999999, 20.300000000000001, new DateTime(2025, 11, 10, 17, 45, 0, 0, DateTimeKind.Unspecified), 4, 5 },
                    { 5, 60.200000000000003, 35.600000000000001, new DateTime(2026, 4, 25, 18, 55, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 6, 72.099999999999994, 18.699999999999999, new DateTime(2027, 9, 5, 19, 30, 0, 0, DateTimeKind.Unspecified), 3, 2 },
                    { 7, 64.799999999999997, 42.299999999999997, new DateTime(2028, 2, 15, 20, 15, 0, 0, DateTimeKind.Unspecified), 4, 1 },
                    { 8, 91.5, 10.199999999999999, new DateTime(2029, 7, 30, 21, 5, 0, 0, DateTimeKind.Unspecified), 5, 4 },
                    { 9, 38.600000000000001, 12.4, new DateTime(2030, 1, 10, 22, 20, 0, 0, DateTimeKind.Unspecified), 2, 5 },
                    { 10, 55.299999999999997, 28.899999999999999, new DateTime(2031, 6, 20, 23, 40, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 11, 80.700000000000003, 15.199999999999999, new DateTime(2032, 12, 5, 0, 10, 0, 0, DateTimeKind.Unspecified), 4, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
