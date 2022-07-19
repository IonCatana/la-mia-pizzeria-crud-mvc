using la_mia_pizzeria_model.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_model.Data
{
    public class PizzeContext : DbContext
    {
       
        public DbSet<Pizze> Pizze{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=db-pizza;Integrated Security=True");
        }
    }
}
