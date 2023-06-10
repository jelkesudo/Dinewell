using Dinewell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dinewell.DataAccess
{
    public class DinewellContext : DbContext
    {
        public DinewellContext()
        {

        }
        public DinewellContext(DbContextOptions opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        public Application.IApplicationActor User { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Dinewell;Integrated Security=True")
                .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Role>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<RoleUseCase>().HasKey(x => new { x.RoleId, x.UseCaseId });
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = User?.Username;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<RestaurantFoodCategory> RestaurantFoodCategories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<RestaurantMenu> RestaurantMenus { get; set; }
        public DbSet<Side> Sides { get; set; }
        public DbSet<RestaurantSide> RestaurantSides { get; set; }
        public DbSet<MenuPrice> MenuPrices { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<SidePrice> SidePrices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMeal> OrderMeals { get; set; }
        public DbSet<OrderMealSide> OrderMealSides { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUseCase> RoleUseCases { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
    } 
}
