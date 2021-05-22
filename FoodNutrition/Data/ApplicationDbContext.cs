using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodAttribute> FoodAttributes { get; set; }
        public DbSet<FoodNutrient> FoodNutrients { get; set; }
        public DbSet<Nutrient> Nutrients { get; set; }
        public DbSet<Portion> Portions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().ToTable("Admin").HasKey(a => a.AdminId);
            modelBuilder.Entity<Category>().ToTable("Category").HasKey(c => c.CategoryId);
            modelBuilder.Entity<Food>().ToTable("Food").HasKey(f => f.FoodId);
            modelBuilder.Entity<FoodAttribute>().ToTable("FoodAttribute").HasKey(f => f.FoodAttributeId);
            modelBuilder.Entity<FoodNutrient>().ToTable("FoodNutrient").HasKey(f => f.FoodNutrientId);
            modelBuilder.Entity<Nutrient>().ToTable("Nutrient").HasKey(n=>n.NutrientId);
            modelBuilder.Entity<Portion>().ToTable("Portion").HasKey(p=>p.PortionId);
        }
    }
}
