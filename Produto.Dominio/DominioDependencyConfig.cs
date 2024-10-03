using Dominio.Servicos.Fornecedor.BuscaPorId;
using Dominio.Servicos.Produto.BuscaPorId;
using Dominio.Servicos.Produto.Cadastrar;
using Dominio.Usecases.Produto.Cadastrar;

namespace Microsoft.Extensions.DependencyInjection;

public static class DominioDependencyConfig
{
    public static void AddDomainDependencies(this IServiceCollection services)
    {
        services.AddScoped<IProdutoCadastroDomainService, ProdutoCadastroDomainService>();
        services.AddScoped<IProdutoBuscaPorIdDomainService, ProdutoBuscaPorIdDomainService>();


        services.AddScoped<IFornecedorBuscaPorIdDomainService, FornecedorBuscaPorIdDomainService>();
    }
}