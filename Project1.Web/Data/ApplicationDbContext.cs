using Microsoft.EntityFrameworkCore;
using Project1.Web.Models;


namespace Project1.Web.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Customers> Customers { get; set; }

        public DbSet<Foodmenu> Foodmenu { get; set; }
        public DbSet<Orders> Orders { get; set; }

        public DbSet<FoodCategory> FoodCategory { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    }


   
}
