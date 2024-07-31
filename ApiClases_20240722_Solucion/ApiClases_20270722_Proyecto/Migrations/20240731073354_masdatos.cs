using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiClases_20270722_Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class masdatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Apellidos", "FechaNacimiento", "Nombre", "Pais", "Usuario" },
                values: new object[,]
                {
                    { 6, "Obama", new DateTime(1961, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barack", "Estados Unidos", "baracko" },
                    { 7, "Messi", new DateTime(1987, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lionel", "Argentina", "leomessi" },
                    { 8, "Musk", new DateTime(1971, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elon", "Sudáfrica", "elonmusk" },
                    { 9, "Winfrey", new DateTime(1954, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oprah", "Estados Unidos", "oprahw" },
                    { 10, "Gates", new DateTime(1955, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bill", "Estados Unidos", "billgates" },
                    { 11, "Merkel", new DateTime(1954, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Angela", "Alemania", "angelam" },
                    { 12, "Ripoll", new DateTime(1977, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shakira", "Colombia", "shakira" },
                    { 13, "Jobs", new DateTime(1955, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Steve", "Estados Unidos", "stevejobs" },
                    { 14, "Zuckerberg", new DateTime(1984, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mark", "Estados Unidos", "markz" },
                    { 15, "Williams", new DateTime(1981, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serena", "Estados Unidos", "serenaw" },
                    { 16, "Ronaldo", new DateTime(1985, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cristiano", "Portugal", "cr7" },
                    { 17, "James", new DateTime(1984, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "LeBron", "Estados Unidos", "lebronj" },
                    { 18, "Kagan", new DateTime(1960, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elena", "Estados Unidos", "elenak" },
                    { 19, "Rowling", new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.K.", "Reino Unido", "jkrowling" },
                    { 20, "Johnson", new DateTime(1972, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dwayne", "Estados Unidos", "therock" }
                });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 1000.5, 1130.5699999999999, new DateTime(2015, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 7 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 2000.75, 2560.96, new DateTime(2018, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 9 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 1500.25, 1740.29, new DateTime(2020, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 11 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 2500.3299999999999, 3500.46, new DateTime(2011, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 13 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 3000.6700000000001, 4260.9499999999998, new DateTime(2017, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 15 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 4000.9899999999998, 2920.73, new DateTime(2021, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 17 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 500.44999999999999, 790.58000000000004, new DateTime(2012, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, 19 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 600.35000000000002, 888.48000000000002, new DateTime(2019, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 6 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 700.12, 1085.55, new DateTime(2014, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 8 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 800.75, 1240.9200000000001, new DateTime(2016, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 10 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 900.63, 1350.3599999999999, new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 12 });

            migrationBuilder.InsertData(
                table: "Transacciones",
                columns: new[] { "Id", "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[,]
                {
                    { 12, 100.90000000000001, 140.72999999999999, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 14 },
                    { 13, 200.33000000000001, 246.40000000000001, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 16 },
                    { 14, 300.5, 402.67000000000002, new DateTime(2013, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 18 },
                    { 15, 400.80000000000001, 548.34000000000003, new DateTime(2020, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 20 },
                    { 16, 5000.9499999999998, 7250.4399999999996, new DateTime(2010, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 7 },
                    { 17, 3500.4499999999998, 4270.8699999999999, new DateTime(2017, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 9 },
                    { 18, 6000.25, 7680.3500000000004, new DateTime(2015, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 11 },
                    { 19, 4200.6700000000001, 4980.1199999999999, new DateTime(2019, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 13 },
                    { 20, 4800.8500000000004, 6960.3999999999996, new DateTime(2011, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 15 },
                    { 21, 5500.25, 8030.5, new DateTime(2021, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 17 },
                    { 22, 3100.3400000000001, 4327.1499999999996, new DateTime(2018, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, 19 },
                    { 23, 2900.75, 3357.8600000000001, new DateTime(2014, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 6 },
                    { 24, 1700.4400000000001, 2400.6300000000001, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 8 },
                    { 25, 650.91999999999996, 995.71000000000004, new DateTime(2012, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 10 },
                    { 26, 4500.5500000000002, 6120.8800000000001, new DateTime(2020, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 12 },
                    { 27, 3800.6799999999998, 4346.7799999999997, new DateTime(2022, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 14 },
                    { 28, 2700.4499999999998, 3159.52, new DateTime(2013, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 16 },
                    { 29, 1900.3399999999999, 2652.8699999999999, new DateTime(2016, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 18 },
                    { 30, 5100.1499999999996, 6120.5699999999997, new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 50.5, 30.199999999999999, new DateTime(2022, 3, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), 1, 2 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 70.299999999999997, 45.799999999999997, new DateTime(2023, 8, 20, 15, 10, 0, 0, DateTimeKind.Unspecified), 2, 3 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 25.100000000000001, 15.699999999999999, new DateTime(2024, 5, 5, 16, 20, 0, 0, DateTimeKind.Unspecified), 3, 4 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 40.899999999999999, 20.300000000000001, new DateTime(2025, 11, 10, 17, 45, 0, 0, DateTimeKind.Unspecified), 4, 5 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 60.200000000000003, 35.600000000000001, new DateTime(2026, 4, 25, 18, 55, 0, 0, DateTimeKind.Unspecified), 5, 1 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 72.099999999999994, 18.699999999999999, new DateTime(2027, 9, 5, 19, 30, 0, 0, DateTimeKind.Unspecified), 3, 2 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 64.799999999999997, 42.299999999999997, new DateTime(2028, 2, 15, 20, 15, 0, 0, DateTimeKind.Unspecified), 4, 1 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 91.5, 10.199999999999999, new DateTime(2029, 7, 30, 21, 5, 0, 0, DateTimeKind.Unspecified), 5, 4 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 38.600000000000001, 12.4, new DateTime(2030, 1, 10, 22, 20, 0, 0, DateTimeKind.Unspecified), 2, 5 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 55.299999999999997, 28.899999999999999, new DateTime(2031, 6, 20, 23, 40, 0, 0, DateTimeKind.Unspecified), 1, 3 });

            migrationBuilder.UpdateData(
                table: "Transacciones",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CantidadEnvia", "CantidadRecibe", "Fecha", "IdEnvia", "IdRecibe" },
                values: new object[] { 80.700000000000003, 15.199999999999999, new DateTime(2032, 12, 5, 0, 10, 0, 0, DateTimeKind.Unspecified), 4, 2 });
        }
    }
}
