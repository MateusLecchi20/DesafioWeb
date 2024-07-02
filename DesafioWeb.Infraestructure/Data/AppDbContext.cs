using DesafioWeb.Domain.Clients.Entities;
using DesafioWeb.Domain.ValueObects;
using Microsoft.EntityFrameworkCore;

namespace DesafioWeb.Infraestructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Endereco> Enderecos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Configurar a relação com Endereco
                entity.HasOne(e => e.Endereco)
                      .WithMany()
                      .HasForeignKey(e => e.EnderecoId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Outras configurações de propriedades, se necessário
                entity.Property(e => e.Codigo).IsRequired();
                entity.Property(e => e.Tipo).IsRequired();
                entity.Property(e => e.CpfCnpj).IsRequired();
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Telefone).IsRequired();
            });

            // Configuração da entidade Endereco
            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Outras configurações de propriedades, se necessário
                entity.Property(e => e.Cep).IsRequired();
                entity.Property(e => e.Logradouro).IsRequired();
                entity.Property(e => e.Numero).IsRequired();
                entity.Property(e => e.Bairro).IsRequired();
                entity.Property(e => e.Municipio).IsRequired();
                entity.Property(e => e.UnidadeFederativa).IsRequired();
            });
        }
    }
}
