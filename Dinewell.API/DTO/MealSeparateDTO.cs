namespace Dinewell.API.DTO
{
    public class MealSeparateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public int FoodCategoryId { get; set; }
        public string FoodCategoryName { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
    }
}
