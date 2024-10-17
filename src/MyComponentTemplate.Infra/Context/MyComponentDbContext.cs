using Microsoft.EntityFrameworkCore;

namespace MyComponentTemplate.Infra.Context
{
    public class MyComponentDbContext(DbContextOptions<MyComponentDbContext> options) : DbContext(options)
    {

        // Defina as tabelas do banco de dados através de DbSet
        public DbSet<MyComponent> MyComponents { get; set; }

        // Configuração de entidades no método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração adicional de mapeamento de entidades
            modelBuilder.Entity<MyComponent>().ToTable("MyComponents");
            modelBuilder.Entity<MyComponent>().HasKey(c => c.Id);

            // Configuração de outras entidades, se necessário
        }
    }

    public class MyComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
