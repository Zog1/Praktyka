using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class ChangedNameOfColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "NumberIdentificate",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "NumberIdentificate",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Brand", "DateCreated", "DateModified", "ImagePath", "IsDeleted", "Model", "ModifiedBy", "NumberOfDoor", "NumberOfSits", "RegistrationNumber", "TypeOfCar", "YearOfProduction" },
                values: new object[] { 1, "Audi", new DateTime(2020, 8, 10, 19, 27, 2, 36, DateTimeKind.Local).AddTicks(9254), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://pngimg.com/uploads/audi/audi_PNG1737.png", false, "Q5", null, 5, 5, "SZE4562", 1, 2019 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CodeOfVerification", "DateCreated", "DateModified", "Email", "FirstName", "HashPassword", "IsDeleted", "LastName", "MobileNumber", "ModifiedBy", "NumberIdentificate", "RoleOfUser", "Salt", "StatusOfVerification" },
                values: new object[] { 2, "37ys", new DateTime(2020, 8, 10, 19, 27, 2, 42, DateTimeKind.Local).AddTicks(4676), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sensacjea@gmail.com", "Bohdan", null, false, "Doe", "458963254", null, "000000", 0, null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CodeOfVerification", "DateCreated", "DateModified", "Email", "FirstName", "HashPassword", "IsDeleted", "LastName", "MobileNumber", "ModifiedBy", "NumberIdentificate", "RoleOfUser", "Salt", "StatusOfVerification" },
                values: new object[] { 1, "xxX3", new DateTime(2020, 8, 10, 19, 27, 2, 42, DateTimeKind.Local).AddTicks(6287), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "zanbor3@gmail.com", "John", null, false, "Doe", "458963254", null, "233Xs5", 1, null, null });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CarId", "DateCreated", "DateModified", "IsFinished", "ModifiedBy", "RentalDate", "ReturnDate", "UserId" },
                values: new object[] { 1, 1, new DateTime(2020, 8, 10, 19, 27, 2, 42, DateTimeKind.Local).AddTicks(8377), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2020, 8, 12, 19, 27, 2, 42, DateTimeKind.Local).AddTicks(7023), new DateTime(2020, 8, 15, 19, 27, 2, 42, DateTimeKind.Local).AddTicks(7387), 1 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "DateCreated", "DateModified", "IsActual", "Latitude", "Longitude", "ModifiedBy", "ReservationId" },
                values: new object[] { 1, new DateTime(2020, 8, 10, 19, 27, 2, 43, DateTimeKind.Local).AddTicks(69), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 50.5, 43.299999999999997, null, 1 });
        }
    }
}
