

namespace WebApplication1.Models
    
    
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }

     
        
        public DbSet<Food>? Foods { get; set; }
        public DbSet<Meal>? Meals { get; set; }
        public DbSet<FoodType>? FoodTypes { get; set; }
        
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FoodType>().HasMany(t => t.Foods).WithOne(f => f.FoodType);
            builder.Entity<FoodType>().HasData(
            new FoodType()
            {
                Id = 1,
                Name = "Meat",
               
            },

            new FoodType()
            {
                Id = 2,
                Name = "Milk and Dairy products"
            },
            new FoodType()
            {
                Id = 3,
                Name = "Eggs"
            },
            new FoodType()
            {
                Id = 4,
                Name = "Cereals",
               

            },
            new FoodType()
            {
                Id = 5,
                Name = "Fish "
            },
            new FoodType()
            {
                Id = 6,
                Name = "Fruits"
            },

            new FoodType()
            {
                Id = 7,
                Name = "Vegetables"
            },

            new FoodType()
            {
                Id = 8,
                Name = "Nuts and seeds"
            },
            new FoodType()
            {
                Id = 9,
                Name = "Drinks and beverages"
            },
            new FoodType()
            {
                Id = 10,
                Name = "Sweets"
            },
            new FoodType()
            {
                Id = 11,
                Name = "Cereals"
            },
            new FoodType()
            {
                Id = 12,
                Name = "Frozen"
            },
            new FoodType()
            {
                Id = 13,
                Name = "Sauces and spices"
            },
            new FoodType()
            {
                Id = 14,
                Name = "Baked and snackes"

            },
            new FoodType()
            {
                Id = 15,
                Name = "Pastas and breads"
            },
            new FoodType()
            {
                Id = 16,
                Name = "Fast food"
            },
            new FoodType()
            {
                Id = 17,
                Name = "Custom"
            }
            );

           
            builder.Entity<Food>().HasMany(m => m.Meals).WithMany(f => f.Foods);

            builder.Entity<Food>().HasData(new Food()
            {
                Id = 1,
                Name = "Pizza",
                Amount = 100,
                CarboHydrates = 60,
                Fats = 25,
                Proteins = 7,
                Kcals = 700,
                FoodTypeId = 16,

            },
            new Food()
            {
                Id = 2,
                Name = "Burger",
                Amount = 100,
                CarboHydrates = 40,
                Fats = 25,
                Proteins = 10,
                Kcals = 600,
                FoodTypeId = 17
            },
            new Food()
            {
                Id = 3,
                Name = "Chicken white ",
                Amount = 100,
                CarboHydrates = 1,
                Fats = 3,
                Proteins = 20,
                Kcals = 600,
                FoodTypeId = 1
            });


            builder.Entity<Meal>().HasMany(f => f.Foods).WithMany(m => m.Meals);


            
            base.OnModelCreating(builder);
        }
    }
}
