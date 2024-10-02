using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.BancoDados.Mapeamentos;

public class ProdutoConfiguracao : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable(nameof(Produto));

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(propriedade => propriedade.Situacao)
            .IsRequired();

        builder
            .Property(propriedade => propriedade.DataFabricacao)
            .IsRequired();

        builder
            .Property(propriedade => propriedade.DataValidade)
            .IsRequired();

        builder
            .HasOne(propriedade => propriedade.Fornecedor)
            .WithMany(fornecedor => fornecedor.Produtos)
            .HasForeignKey(produto => produto.IdFornecedor)
            .IsRequired();
    }
}