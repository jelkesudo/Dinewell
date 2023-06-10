using BCrypt.Net;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {
        private DinewellContext _context;
        public InitialDataController(DinewellContext context)
        {
            _context = context;
        }
        // POST api/<InitialDataController>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post()
        {
            try
            {
                if (_context.Restaurants.Any())
                {
                    return Conflict(new { message = "Data already inserted." });
                }
                var restaurants = new List<Restaurant>
            {
                new Restaurant{ Name = "Pizza place", Description="Best pizza in town and further", Address="Zmaj jovina", AddressNumber = 16, WorkFrom = 8, WorkTo = 20},
                new Restaurant{ Name = "Hamburger Lord", Description="Burger like no other", Address="Kosovska", AddressNumber = 25, WorkFrom = 10, WorkTo = 21},
                new Restaurant{ Name = "Golden fish", Description="All the fish you can eat", Address="Makedonska", AddressNumber = 78, WorkFrom = 6, WorkTo = 18},
            };
                var foodCategories = new List<FoodCategory>
            {
                new FoodCategory{ Name = "Pizza"},
                new FoodCategory{ Name = "Fish"},
                new FoodCategory{ Name = "Burger"},
                new FoodCategory{ Name = "Tortilla"},
            };
                var foods = new List<Food>
            {
                new Food{ Name = "XXL Pizza"},
                new Food{ Name = "XL Pizza"},
                new Food{ Name = "l Pizza"},
                new Food{ Name = "S Pizza"},
                new Food{ Name = "Pizza bread"},
                new Food{ Name = "Hamburger"},
                new Food{ Name = "Cheeseburger"},
                new Food{ Name = "Double Cheeseburger"},
                new Food{ Name = "Triple Cheeseburger"},
                new Food{ Name = "Fish Burger"},
                new Food{ Name = "Tortilla Wrap"},
                new Food{ Name = "Salmon"},
                new Food{ Name = "Acrogiali"},
                new Food{ Name = "Catfish"},
                new Food{ Name = "Zander"},
                new Food{ Name = "Bull Trout"},
                new Food{ Name = "Bass"},
                new Food{ Name = "Sea bream"},
                new Food{ Name = "Grouper"},
            };

                var restaurantFoodCategories = new List<RestaurantFoodCategory>
            {
                new RestaurantFoodCategory{Restaurant = restaurants.First(), FoodCategory = foodCategories.First()},
                new RestaurantFoodCategory{Restaurant = restaurants.ElementAt(1), FoodCategory = foodCategories.ElementAt(2)},
                new RestaurantFoodCategory{Restaurant = restaurants.ElementAt(1), FoodCategory = foodCategories.ElementAt(3)},
                new RestaurantFoodCategory{Restaurant = restaurants.ElementAt(2), FoodCategory = foodCategories.ElementAt(1)},
            };

                var restaurantMenus = new List<RestaurantMenu>
            {
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.First(), Food = foods.First(), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.First(), Food = foods.ElementAt(1), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.First(), Food = foods.ElementAt(2), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.First(), Food = foods.ElementAt(3), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.First(), Food = foods.ElementAt(4), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Food = foods.ElementAt(5),Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Food = foods.ElementAt(6), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Food = foods.ElementAt(7), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Food = foods.ElementAt(8), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Food = foods.ElementAt(9), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(2), Food = foods.ElementAt(10), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Food = foods.ElementAt(11), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Food = foods.ElementAt(12), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Food = foods.ElementAt(13), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Food = foods.ElementAt(14), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Food = foods.ElementAt(15), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Food = foods.ElementAt(16), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Food = foods.ElementAt(17), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
                new RestaurantMenu{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Food = foods.ElementAt(18), Description = "Description for the given food, like ingredients and how many grams, for the sake of finishing the project this will be the same for all food"},
            };

                var menuPrices = new List<MenuPrice>
            {
                new MenuPrice{Food = restaurantMenus.First(), Price = 20.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(1), Price = 20.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(2), Price = 15.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(3), Price = 18.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(4), Price = 8.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(5), Price = 4.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(6), Price = 2.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(7), Price = 11.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(8), Price = 24.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(9), Price = 14.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(10), Price = 20.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(11), Price = 13.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(12), Price = 12.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(13), Price = 14.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(14), Price = 18.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(15), Price = 5.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(16), Price = 17.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(17), Price = 19.99m},
                new MenuPrice{Food = restaurantMenus.ElementAt(18), Price = 13.99m},
            };
                var roles = new List<Role> {
                new Role{Name = "Admin"},
                new Role{Name = "User"},
            };
                var users = new List<User>
            {
                new User{Username = "Mare", FirstName = "Marko", LastName = "Markovic", Email = "marko@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("Probaacc123#"), Role = roles.ElementAt(1)},
                new User{Username = "Zare", FirstName = "Zarko", LastName = "Dakic", Email = "zarko@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("Probaacc123#"), Role = roles.ElementAt(1)},
                new User{Username = "Janchi01", FirstName = "Jana", LastName = "Jankovic", Email = "jana@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("Probaacc123#"), Role = roles.ElementAt(0)},
            };
                var sides = new List<Side>
            {
                new Side { Name= "Extra Pepperoni"},
                new Side { Name= "Extra Cheese"},
                new Side { Name= "Mushrooms"},
                new Side { Name= "Onions"},
                new Side { Name= "Sausage"},
                new Side { Name= "Black Olives"},
                new Side { Name= "Green Peppers"},
                new Side { Name= "Pineapple"},
                new Side { Name= "Spinach"},
                new Side { Name= "Lettuce"},
                new Side { Name= "Avocado"},
                new Side { Name= "Tomato"},
                new Side { Name= "Pickles"},
                new Side { Name= "Fried Egg"},
                new Side { Name= "Coleslaw"},
                new Side { Name= "Bacon"},
                new Side { Name= "Cream cheese"},
                new Side { Name= "Ketchup"},
                new Side { Name= "Mustard"},
                new Side { Name= "Mayonaise"},
                new Side { Name= "BBQ Sause"},
                new Side { Name= "Russian Dressing"},
                new Side { Name= "Pickle Relish"},
                new Side { Name= "Jalapeno peppers"},
                new Side { Name= "Lemon"},
                new Side { Name= "Parsley Sause"},
                new Side { Name= "Hollandaise Sauce"},
                new Side { Name= "Romesco Sause"},
            };

                var restaurantSides = new List<RestaurantSide>
            {
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(0), Side = sides.ElementAt(0)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(0), Side = sides.ElementAt(1)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(0), Side = sides.ElementAt(2)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(0), Side = sides.ElementAt(3)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(0), Side = sides.ElementAt(4)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(0), Side = sides.ElementAt(5)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(0), Side = sides.ElementAt(6)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(0), Side = sides.ElementAt(7)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(0), Side = sides.ElementAt(8)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(9)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(10)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(11)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(12)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(13)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(14)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(15)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(16)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(17)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(18)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(19)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(20)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(21)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(22)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(1), Side = sides.ElementAt(23)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Side = sides.ElementAt(24)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Side = sides.ElementAt(25)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Side = sides.ElementAt(26)},
                new RestaurantSide{RestaurantFoodCategory = restaurantFoodCategories.ElementAt(3), Side = sides.ElementAt(27)},
            };

                var restaurantSidePrices = new List<SidePrice>
            {
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(0), Price = 5},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(1), Price = 6},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(2), Price = 7},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(3), Price = 12},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(4), Price = 4},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(5), Price = 8},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(6), Price = 7},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(7), Price = 9},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(8), Price = 3},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(9), Price = 4},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(10), Price = 11},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(11), Price = 10},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(12), Price = 8},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(13), Price = 12},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(14), Price = 4},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(15), Price = 9},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(16), Price = 5},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(17), Price = 7},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(18), Price = 6},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(19), Price = 3},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(20), Price = 2},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(21), Price = 15},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(22), Price = 8},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(23), Price = 9},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(24), Price = 1},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(25), Price = 10},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(26), Price = 6},
                new SidePrice{RestaurantSide = restaurantSides.ElementAt(27), Price = 12.5m},
            };

                var orders = new List<Order>
            {
                new Order{User = users.ElementAt(0), OrderNumber = Guid.NewGuid(), OrderAddress = "Makedonska 17"},
                new Order{User = users.ElementAt(0), OrderNumber = Guid.NewGuid(), OrderAddress = "Vuka Karadzica 222"},
                new Order{User = users.ElementAt(1), OrderNumber = Guid.NewGuid(), OrderAddress = "Vojvode Misica 56"},
                new Order{User = users.ElementAt(2), OrderNumber = Guid.NewGuid(), OrderAddress = "Jurija Gagarina 28"},
            };

                var orderMeals = new List<OrderMeal>
            {
                new OrderMeal{ Order = orders.First(), Meal = restaurantMenus.ElementAt(2), Quantity = 1},
                new OrderMeal{ Order = orders.First(), Meal = restaurantMenus.ElementAt(15), Quantity = 1},
                new OrderMeal{ Order = orders.ElementAt(1), Meal = restaurantMenus.ElementAt(12), Quantity = 1},
                new OrderMeal{ Order = orders.ElementAt(2), Meal = restaurantMenus.ElementAt(17), Quantity = 2},
            };
                var roleUseCases = new List<RoleUseCase>();

                for (int i = 2; i <= 10; i++)
                {
                    roleUseCases.Add(new RoleUseCase { Role = roles.ElementAt(1), UseCaseId = i });
                }
                for (int i = 2; i <= 44; i++)
                {
                    roleUseCases.Add(new RoleUseCase { Role = roles.ElementAt(0), UseCaseId = i });
                }
                _context.Restaurants.AddRange(restaurants);
                _context.FoodCategories.AddRange(foodCategories);
                _context.Foods.AddRange(foods);
                _context.RestaurantFoodCategories.AddRange(restaurantFoodCategories);
                _context.RestaurantMenus.AddRange(restaurantMenus);
                _context.Roles.AddRange(roles);
                _context.Users.AddRange(users);
                _context.Sides.AddRange(sides);
                _context.RestaurantSides.AddRange(restaurantSides);
                _context.SidePrices.AddRange(restaurantSidePrices);
                _context.MenuPrices.AddRange(menuPrices);
                _context.Orders.AddRange(orders);
                _context.OrderMeals.AddRange(orderMeals);
                _context.RoleUseCases.AddRange(roleUseCases);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "There was an error with inserting initial data."});
            }
        }
    }
}
