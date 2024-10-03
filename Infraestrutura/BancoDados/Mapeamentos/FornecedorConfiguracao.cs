using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.BancoDados.Mapeamentos;

public class FornecedorConfiguracao : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.ToTable(nameof(Fornecedor));

        builder.Ignore(p => p.Notifications);

        builder.HasKey(c => c.Id);

        builder
           .Property(propriedade => propriedade.Descricao)
           .IsRequired();

        builder
           .Property(propriedade => propriedade.Cnpj)
           .HasMaxLength(14)
           .IsRequired();
    }
}