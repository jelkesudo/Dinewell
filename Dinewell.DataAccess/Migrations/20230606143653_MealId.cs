using Microsoft.EntityFrameworkCore.Migrations;

namespace Dinewell.DataAccess.Migrations
{
    public partial class MealId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderMeals_RestaurantMenus_MealsId",
                table: "OrderMeals");

            migrationBuilder.DropIndex(
                name: "IX_OrderMeals_MealsId",
                table: "OrderMeals");

            migrationBuilder.DropColumn(
                name: "MealsId",
                table: "OrderMeals");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMeals_MealId",
                table: "OrderMeals",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMeals_RestaurantMenus_MealId",
                table: "OrderMeals",
                column: "MealId",
                principalTable: "RestaurantMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderMeals_RestaurantMenus_MealId",
                table: "OrderMeals");

            migrationBuilder.DropIndex(
                name: "IX_OrderMeals_MealId",
                table: "OrderMeals");

            migrationBuilder.AddColumn<int>(
                name: "MealsId",
                table: "OrderMeals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderMeals_MealsId",
                table: "OrderMeals",
                column: "MealsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMeals_RestaurantMenus_MealsId",
                table: "OrderMeals",
                column: "MealsId",
                principalTable: "RestaurantMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
