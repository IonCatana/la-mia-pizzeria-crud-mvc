using la_mia_pizzeria_model.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_model.Data
{
    public class PizzeContext : DbContext
    {
       
        public DbSet<Pizze>? Pizze{ get; set; }
        public DbSet<Category>? Category { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=db-pizza;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, NomeCategoria = "Pizze classiche" },               
                new Category { Id = 4, NomeCategoria = "Pizze di mare" });
        }
    }
}
