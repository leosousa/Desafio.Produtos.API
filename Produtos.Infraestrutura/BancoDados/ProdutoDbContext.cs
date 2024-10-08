using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infraestrutura.BancoDados;

public class ProdutoDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }

    public DbSet<Fornecedor> Fornecedores { get; set; }

    public ProdutoDbContext()
    {
    }

    public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}