using Microsoft.EntityFrameworkCore;
using MyComponentTemplate.Infra.Context;

namespace MyComponentTemplate.Infra.MySql
{
    public class MyComponentDbContext : DbContext
    {
        public MyComponentDbContext(DbContextOptions<MyComponentDbContext> options)
            : base(options)
        {
        }

        public DbSet<MyComponent> MyComponents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyComponent>().ToTable("MyComponents");
        }
    }

}
