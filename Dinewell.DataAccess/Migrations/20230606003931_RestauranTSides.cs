using Microsoft.EntityFrameworkCore.Migrations;

namespace Dinewell.DataAccess.Migrations
{
    public partial class RestauranTSides : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderMealSides_RestauranSides_SideId",
                table: "OrderMealSides");

            migrationBuilder.DropForeignKey(
                name: "FK_RestauranSides_RestaurantFoodCategories_RestaurantFoodCategoryId",
                table: "RestauranSides");

            migrationBuilder.DropForeignKey(
                name: "FK_RestauranSides_Sides_SideId",
                table: "RestauranSides");

            migrationBuilder.DropForeignKey(
                name: "FK_SidePrices_RestauranSides_SideId",
                table: "SidePrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestauranSides",
                table: "RestauranSides");

            migrationBuilder.RenameTable(
                name: "RestauranSides",
                newName: "RestaurantSides");

            migrationBuilder.RenameIndex(
                name: "IX_RestauranSides_SideId",
                table: "RestaurantSides",
                newName: "IX_RestaurantSides_SideId");

            migrationBuilder.RenameIndex(
                name: "IX_RestauranSides_RestaurantFoodCategoryId",
                table: "RestaurantSides",
                newName: "IX_RestaurantSides_RestaurantFoodCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantSides",
                table: "RestaurantSides",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMealSides_RestaurantSides_SideId",
                table: "OrderMealSides",
                column: "SideId",
                principalTable: "RestaurantSides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantSides_RestaurantFoodCategories_RestaurantFoodCategoryId",
                table: "RestaurantSides",
                column: "RestaurantFoodCategoryId",
                principalTable: "RestaurantFoodCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantSides_Sides_SideId",
                table: "RestaurantSides",
                column: "SideId",
                principalTable: "Sides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SidePrices_RestaurantSides_SideId",
                table: "SidePrices",
                column: "SideId",
                principalTable: "RestaurantSides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderMealSides_RestaurantSides_SideId",
                table: "OrderMealSides");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantSides_RestaurantFoodCategories_RestaurantFoodCategoryId",
                table: "RestaurantSides");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantSides_Sides_SideId",
                table: "RestaurantSides");

            migrationBuilder.DropForeignKey(
                name: "FK_SidePrices_RestaurantSides_SideId",
                table: "SidePrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantSides",
                table: "RestaurantSides");

            migrationBuilder.RenameTable(
                name: "RestaurantSides",
                newName: "RestauranSides");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantSides_SideId",
                table: "RestauranSides",
                newName: "IX_RestauranSides_SideId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantSides_RestaurantFoodCategoryId",
                table: "RestauranSides",
                newName: "IX_RestauranSides_RestaurantFoodCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestauranSides",
                table: "RestauranSides",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMealSides_RestauranSides_SideId",
                table: "OrderMealSides",
                column: "SideId",
                principalTable: "RestauranSides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestauranSides_RestaurantFoodCategories_RestaurantFoodCategoryId",
                table: "RestauranSides",
                column: "RestaurantFoodCategoryId",
                principalTable: "RestaurantFoodCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestauranSides_Sides_SideId",
                table: "RestauranSides",
                column: "SideId",
                principalTable: "Sides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SidePrices_RestauranSides_SideId",
                table: "SidePrices",
                column: "SideId",
                principalTable: "RestauranSides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
