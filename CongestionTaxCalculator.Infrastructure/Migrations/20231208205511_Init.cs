using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CongestionTaxCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    MaxDailyCharge_Amount = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    MaxDailyCharge_Currency = table.Column<string>(type: "char(3)", nullable: true),
                    TollFreeDays = table.Column<byte>(type: "tinyint", nullable: true),
                    SingleChargeRuleMinutes = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Charge_Amount = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    Charge_Currency = table.Column<string>(type: "char(3)", nullable: false),
                    TimeRange_Start = table.Column<TimeOnly>(type: "time(0)", nullable: false),
                    TimeRange_End = table.Column<TimeOnly>(type: "time(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxRules_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TollFreeDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TollFreeDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TollFreeDates_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExemptCityVehicles",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExemptCityVehicles", x => new { x.VehicleId, x.CityId });
                    table.ForeignKey(
                        name: "FK_ExemptCityVehicles_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExemptCityVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "SingleChargeRuleMinutes", "TollFreeDays", "MaxDailyCharge_Amount", "MaxDailyCharge_Currency" },
                values: new object[] { 1, "Gothenburg", (short)60, (byte)65, 60m,"SEK" });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Car" },
                    { 2, "Emergency" },
                    { 3, "Buss" },
                    { 4, "Diplomat" },
                    { 5, "Motorcycle" },
                    { 6, "Military" },
                    { 7, "Foreign" }
                });

            migrationBuilder.InsertData(
                table: "ExemptCityVehicles",
                columns: new[] { "CityId", "VehicleId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 }
                });

            migrationBuilder.InsertData(
                table: "TaxRules",
                columns: new[] { "Id", "CityId", "Charge_Amount", "Charge_Currency", "TimeRange_Start", "TimeRange_End" },
                values: new object[,]
                {
                    { 1, 1, 0m, "SEK", TimeOnly.MinValue, new TimeOnly(6, 0, 0) },
                    { 2, 1, 8m, "SEK", new TimeOnly(6, 0, 0), new TimeOnly(6, 30, 0) },
                    { 3, 1, 13m, "SEK", new TimeOnly(6, 30, 0), new TimeOnly(7, 0, 0) },
                    { 4, 1, 18m, "SEK", new TimeOnly(7, 0, 0), new TimeOnly(8, 0, 0) },
                    { 5, 1, 13m, "SEK", new TimeOnly(8, 0, 0), new TimeOnly(8, 30, 0) },
                    { 6, 1, 8m, "SEK", new TimeOnly(8, 30, 0), new TimeOnly(15, 0, 0) },
                    { 7, 1, 13m, "SEK", new TimeOnly(15, 0, 0), new TimeOnly(15, 30, 0) },
                    { 8, 1, 18m, "SEK", new TimeOnly(15, 30, 0), new TimeOnly(17, 0, 0) },
                    { 9, 1, 13m, "SEK", new TimeOnly(17, 0, 0), new TimeOnly(18, 0, 0) },
                    { 10, 1, 8m, "SEK", new TimeOnly(18, 0, 0), new TimeOnly(18, 30, 0) },
                    { 11, 1, 0m, "SEK", new TimeOnly(18, 30, 0), TimeOnly.MaxValue }
                });

            migrationBuilder.InsertData(
                table: "TollFreeDates",
                columns: new[] { "Id", "CityId", "Date" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2013, 1, 1) },
                    { 2, 1, new DateOnly(2013, 1, 5) },
                    { 3, 1, new DateOnly(2013, 1, 6) },
                    { 4, 1, new DateOnly(2013, 3, 28) },
                    { 5, 1, new DateOnly(2013, 3, 29) },
                    { 6, 1, new DateOnly(2013, 3, 30) },
                    { 7, 1, new DateOnly(2013, 3, 31) },
                    { 8, 1, new DateOnly(2013, 4, 1) },
                    { 9, 1, new DateOnly(2013, 4, 30) },
                    { 10, 1, new DateOnly(2013, 5, 1) },
                    { 11, 1, new DateOnly(2013, 5, 8) },
                    { 12, 1, new DateOnly(2013, 5, 9) },
                    { 13, 1, new DateOnly(2013, 5, 18) },
                    { 14, 1, new DateOnly(2013, 5, 19) },
                    { 15, 1, new DateOnly(2013, 5, 20) },
                    { 16, 1, new DateOnly(2013, 6, 21) },
                    { 17, 1, new DateOnly(2013, 6, 22) },
                    { 18, 1, new DateOnly(2013, 7, 1) },
                    { 19, 1, new DateOnly(2013, 7, 2) },
                    { 20, 1, new DateOnly(2013, 7, 3) },
                    { 21, 1, new DateOnly(2013, 7, 4) },
                    { 22, 1, new DateOnly(2013, 7, 5) },
                    { 23, 1, new DateOnly(2013, 7, 6) },
                    { 24, 1, new DateOnly(2013, 7, 7) },
                    { 25, 1, new DateOnly(2013, 7, 8) },
                    { 26, 1, new DateOnly(2013, 7, 9) },
                    { 27, 1, new DateOnly(2013, 7, 10) },
                    { 28, 1, new DateOnly(2013, 7, 11) },
                    { 29, 1, new DateOnly(2013, 7, 12) },
                    { 30, 1, new DateOnly(2013, 7, 13) },
                    { 31, 1, new DateOnly(2013, 7, 14) },
                    { 32, 1, new DateOnly(2013, 7, 15) },
                    { 33, 1, new DateOnly(2013, 7, 16) },
                    { 34, 1, new DateOnly(2013, 7, 17) },
                    { 35, 1, new DateOnly(2013, 7, 18) },
                    { 36, 1, new DateOnly(2013, 7, 19) },
                    { 37, 1, new DateOnly(2013, 7, 20) },
                    { 38, 1, new DateOnly(2013, 7, 21) },
                    { 39, 1, new DateOnly(2013, 7, 22) },
                    { 40, 1, new DateOnly(2013, 7, 23) },
                    { 41, 1, new DateOnly(2013, 7, 24) },
                    { 42, 1, new DateOnly(2013, 7, 25) },
                    { 43, 1, new DateOnly(2013, 7, 26) },
                    { 44, 1, new DateOnly(2013, 7, 27) },
                    { 45, 1, new DateOnly(2013, 7, 28) },
                    { 46, 1, new DateOnly(2013, 7, 29) },
                    { 47, 1, new DateOnly(2013, 7, 30) },
                    { 48, 1, new DateOnly(2013, 7, 31) },
                    { 49, 1, new DateOnly(2013, 10, 31) },
                    { 50, 1, new DateOnly(2013, 11, 1) },
                    { 51, 1, new DateOnly(2013, 12, 24) },
                    { 52, 1, new DateOnly(2013, 12, 25) },
                    { 53, 1, new DateOnly(2013, 12, 26) },
                    { 54, 1, new DateOnly(2013, 12, 30) },
                    { 55, 1, new DateOnly(2013, 12, 31) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExemptCityVehicles_CityId",
                table: "ExemptCityVehicles",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ExemptCityVehicles_VehicleId_CityId",
                table: "ExemptCityVehicles",
                columns: new[] { "VehicleId", "CityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxRules_CityId",
                table: "TaxRules",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TollFreeDates_CityId",
                table: "TollFreeDates",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Name",
                table: "Vehicles",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ExemptCityVehicles");

            migrationBuilder.DropTable(
                name: "TaxRules");

            migrationBuilder.DropTable(
                name: "TollFreeDates");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
