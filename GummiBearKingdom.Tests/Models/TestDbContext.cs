using Microsoft.EntityFrameworkCore;

namespace GummiBearKingdom.Models
{
    public class TestDbContext : GummiBearDbContext
    {
        public override DbSet<Product> Products { get; set; }
        public override DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(@"Server=localhost;port=8889;database=GummiBearKingdom_test;uid=root;pwd=root;");
        }
    }
}