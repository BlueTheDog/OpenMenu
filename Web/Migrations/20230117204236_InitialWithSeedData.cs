using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class InitialWithSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", maxLength: 10, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true),
                    Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: true),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locations_ClientTypes_ClientTypeId",
                        column: x => x.ClientTypeId,
                        principalTable: "ClientTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemTypes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemEntityMenuItemTypeEntity",
                columns: table => new
                {
                    MenuItemTypesId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemEntityMenuItemTypeEntity", x => new { x.MenuItemTypesId, x.MenuItemsId });
                    table.ForeignKey(
                        name: "FK_MenuItemEntityMenuItemTypeEntity_MenuItems_MenuItemsId",
                        column: x => x.MenuItemsId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemEntityMenuItemTypeEntity_MenuItemTypes_MenuItemTypesId",
                        column: x => x.MenuItemTypesId,
                        principalTable: "MenuItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClientTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(49), "Restaurant" });

            migrationBuilder.InsertData(
                table: "ClientTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 2, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(50), "Cafenea" });

            migrationBuilder.InsertData(
                table: "ClientTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 3, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(50), "Atelier auto" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Email", "Name", "Phone" },
                values: new object[] { 1, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(61), "Cafenelele 5 to go", "contact@5togo.ro", "5ToGo", "0761.000.000" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Email", "Name", "Phone" },
                values: new object[] { 2, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(62), "Cafenelele Starbucks", "contact@starbucks.ro", "Starbucks", "0761.111.111" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Email", "Name", "Phone" },
                values: new object[] { 3, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(63), "Restaurantele Marty", "contact@martycluj.ro", "MartyRestaurants", "0761.222.222" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Email", "Name", "Phone" },
                values: new object[] { 4, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(64), "Dacia Service description", "contact@dacia.ro", "Dacia Service", "0761.333.333" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Email", "Name", "Phone" },
                values: new object[] { 5, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(65), "Mancare cu suflet", "contact@boulevard.ro", "Boulevard", "0761.444.444" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Email", "Name", "Phone" },
                values: new object[] { 6, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(66), "Avioanele, ca am mai multe.", "contact@tiriac.ro", "TiriacSerices", "0761.666.666" });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name", "Price" },
                values: new object[] { 1, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(24), "1 shot espresso", "Espresso", 6m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name", "Price" },
                values: new object[] { 2, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(26), "2 shots and milk", "Flat White", 9m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name", "Price" },
                values: new object[] { 3, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(26), "one shot more milk", "Latte", 12m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name", "Price" },
                values: new object[] { 4, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(27), "The CocaCola company", "Coca cola", 6m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name", "Price" },
                values: new object[] { 5, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(28), "not the right one", "Pepsi", 5m });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 1, 1, 2, new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(84), "La 5 to go folosim o cafea cu o aromă intensă, corpolentă , cremoasă ce se constituie într-un blend unic, creat special pentru lanţul de cafenele 5 to go.", null, null, "5ToGo XL" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 2, 2, 2, new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(85), "Starbucks Corporation is an American multinational chain of coffeehouses and roastery reserves headquartered in Seattle, Washington. ", null, null, "Starbucks" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 3, 1, 2, new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(86), "Colaborăm şi susţinem ferme care respectă fructul de cafea, dar mai mult, respectă oamenii implicaţi în povestea cafelei.", null, null, "5ToGo Hunedoara" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 5, 3, 1, new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(86), "Cele șase locații Marty Restaurants sunt asemănătoare în esență, dar fiecare dintre ele propune o atmosferă unică. ", null, null, "Marty Restaurants" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 6, 5, 1, new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(87), "Mereu delicios", null, null, "Boulevard" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 9, 4, 3, new DateTime(2022, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(88), null, null, null, "Dacia Service" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 10, 4, 3, new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(89), null, null, null, "Renault Service" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 11, 6, 3, new DateTime(2022, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(89), null, null, null, "Mercedes Service" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 12, 6, 3, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(90), null, null, null, "BMW Service" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ClientId", "ClientTypeId", "DateCreated", "DateModified", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { 13, 6, 3, new DateTime(2022, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(91), null, null, null, "Tesla Service" });

            migrationBuilder.InsertData(
                table: "MenuItemTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "LocationId", "Name" },
                values: new object[] { 1, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(2), 1, "Cafea" });

            migrationBuilder.InsertData(
                table: "MenuItemTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "LocationId", "Name" },
                values: new object[] { 2, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(4), 1, "Deserturi" });

            migrationBuilder.InsertData(
                table: "MenuItemTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "LocationId", "Name" },
                values: new object[] { 3, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(5), 1, "Cafea boabe" });

            migrationBuilder.InsertData(
                table: "MenuItemTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "LocationId", "Name" },
                values: new object[] { 4, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 17, 20, 42, 36, 289, DateTimeKind.Utc).AddTicks(6), 1, "Racoritoare" });

            migrationBuilder.InsertData(
                table: "MenuItemEntityMenuItemTypeEntity",
                columns: new[] { "MenuItemTypesId", "MenuItemsId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "MenuItemEntityMenuItemTypeEntity",
                columns: new[] { "MenuItemTypesId", "MenuItemsId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "MenuItemEntityMenuItemTypeEntity",
                columns: new[] { "MenuItemTypesId", "MenuItemsId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "MenuItemEntityMenuItemTypeEntity",
                columns: new[] { "MenuItemTypesId", "MenuItemsId" },
                values: new object[] { 4, 4 });

            migrationBuilder.InsertData(
                table: "MenuItemEntityMenuItemTypeEntity",
                columns: new[] { "MenuItemTypesId", "MenuItemsId" },
                values: new object[] { 4, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ClientId",
                table: "Locations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ClientTypeId",
                table: "Locations",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemEntityMenuItemTypeEntity_MenuItemsId",
                table: "MenuItemEntityMenuItemTypeEntity",
                column: "MenuItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemTypes_LocationId",
                table: "MenuItemTypes",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemEntityMenuItemTypeEntity");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "MenuItemTypes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ClientTypes");
        }
    }
}
