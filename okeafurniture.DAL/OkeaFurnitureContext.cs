using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.DAL
{
    public class OkeaFurnitureContext : DbContext
    {
        public DbContextOptions Options { private set; get; }

        public DbSet<Account> Account { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        
        //bridge table
        public DbSet<CartItem> CartItem { get; set; }

        public OkeaFurnitureContext(DbContextOptions options) : base(options)
        {
            Options = options;
        }

        public static OkeaFurnitureContext GetDbContext()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<OkeaFurnitureContext>();
            var config = builder.Build();
            var connectionString = config["ConnectionStrings:OkeaFurniture"];

            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new OkeaFurnitureContext(options);
        }

        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddUserSecrets<OkeaFurnitureContext>();
            var config = builder.Build();

            var connectionString = config["ConnectionStrings:okea-sql-express"];

            return connectionString;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasKey(ci => new { ci.CartId, ci.ItemId });

        }
    }
}
