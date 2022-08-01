using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Amount", "CarboHydrates", "Fats", "FoodTypeId", "Kcals", "Name", "Proteins" },
                values: new object[] { 1, 100.0, 60, 25, 16, 700, "Pizza", 7 });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Amount", "CarboHydrates", "Fats", "FoodTypeId", "Kcals", "Name", "Proteins" },
                values: new object[] { 2, 100.0, 40, 25, 17, 600, "Burger", 10 });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Amount", "CarboHydrates", "Fats", "FoodTypeId", "Kcals", "Name", "Proteins" },
                values: new object[] { 3, 100.0, 1, 3, 1, 600, "Chicken white ", 20 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
