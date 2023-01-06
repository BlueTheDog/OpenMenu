using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuTypes",
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
                    table.PrimaryKey("PK_MenuTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true),
                    MenuTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_MenuTypes_MenuTypeId",
                        column: x => x.MenuTypeId,
                        principalTable: "MenuTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2234), "RestaurantX" });

            migrationBuilder.InsertData(
                table: "MenuTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 2, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2235), "Cafenea" });

            migrationBuilder.InsertData(
                table: "MenuTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 3, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2236), "Atelier auto" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 1, new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2310), "La 5 to go folosim o cafea cu o aromă intensă, corpolentă , cremoasă ce se constituie într-un blend unic, creat special pentru lanţul de cafenele 5 to go.", 2, "5ToGo" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 2, new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2311), "Starbucks Corporation is an American multinational chain of coffeehouses and roastery reserves headquartered in Seattle, Washington. ", 2, "Starbucks" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 3, new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2312), "Colaborăm şi susţinem ferme care respectă fructul de cafea, dar mai mult, respectă oamenii implicaţi în povestea cafelei.", 2, "NarCoffee" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 4, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2313), "O cafea arabica, aromata, tare. Cam tot ce aștepți de la o cafea. Plus ca ii ajuți și pe micii fermieri cumpărând-o. ", 2, "Cafetarie" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 5, new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2313), "Cele șase locații Marty Restaurants sunt asemănătoare în esență, dar fiecare dintre ele propune o atmosferă unică. ", 1, "Marty Restaurants" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 6, new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2314), "Mereu delicios", 1, "Boulevard" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 7, new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2315), "Mâncarea e delicioasa, ca la mama acasă, proaspătă și servita fără întârziere.", 1, "Curtea Veche" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 8, new DateTime(2022, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2315), null, 1, "Casa Romaneasca" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 9, new DateTime(2022, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2316), null, 3, "Dacia Service" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 10, new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2316), null, 3, "Renault Service" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 11, new DateTime(2022, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2317), null, 3, "Mercedes Service" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 12, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2318), null, 3, "BMW Service" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 13, new DateTime(2022, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2318), null, 3, "Tesla Service" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 14, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2319), null, 2, "Cafeneaua de la colt" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 15, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2320), null, 2, "Happy Beans" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 16, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2320), null, 1, "All u can eat" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "MenuTypeId", "Name" },
                values: new object[] { 17, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 15, 16, 11, 40, 206, DateTimeKind.Utc).AddTicks(2321), null, 1, "Moldovan" });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_MenuTypeId",
                table: "Locations",
                column: "MenuTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "MenuTypes");
        }
    }
}
