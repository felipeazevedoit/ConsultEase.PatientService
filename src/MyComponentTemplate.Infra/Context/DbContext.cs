using Microsoft.EntityFrameworkCore;

namespace MyComponentTemplate.Infra.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        // Defina as tabelas do banco de dados através de DbSet
        // public DbSet<MyComponent> MyComponents { get; set; }

        // Configuração de entidades no método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração adicional de mapeamento de entidades
            // modelBuilder.Entity<MyComponent>().ToTable("MyComponents");
            // modelBuilder.Entity<MyComponent>().HasKey(c => c.Id);

            // Configuração de outras entidades, se necessário
        }
    }
}
