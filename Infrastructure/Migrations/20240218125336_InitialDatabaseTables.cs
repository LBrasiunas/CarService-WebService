using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarsAssignedToServices",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsAssignedToServices", x => new { x.ServiceId, x.CarId });
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            // Add data to Cars table
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year" },
                values: new object[] { "Toyota", "Camry", 2022 });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year" },
                values: new object[] { "Honda", "Accord", 2020 });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year" },
                values: new object[] { "Ford", "Fusion", 2019 });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year" },
                values: new object[] { "Chevrolet", "Malibu", 2018 });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Brand", "Model", "Year" },
                values: new object[] { "Tesla", "Model S", 2023 });

            // Add data to Services table
            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Name", "Description" },
                values: new object[] { "Berauto", "We fix everything related to cars" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Name", "Description" },
                values: new object[] { "QuickFix", "Fast and efficient car repairs" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Name", "Description" },
                values: new object[] { "TuneUp", "Get your car running smoothly" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Name", "Description" },
                values: new object[] { "Detailing", "Make your car look brand new" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Name", "Description" },
                values: new object[] { "OilChange", null });

            // Add data to CarsAssignedToServices table
            migrationBuilder.InsertData(
                table: "CarsAssignedToServices",
                columns: new[] { "ServiceId", "CarId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "CarsAssignedToServices",
                columns: new[] { "ServiceId", "CarId" },
                values: new object[] { 2, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarsAssignedToServices");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
