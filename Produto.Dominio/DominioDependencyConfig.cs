using Dominio.Servicos.Fornecedor.BuscaPorId;
using Dominio.Servicos.Produto.BuscaPorId;
using Dominio.Servicos.Produto.Cadastrar;
using Dominio.Servicos.Produto.Editar;
using Dominio.Servicos.Produto.ListaPaginada;
using Dominio.Usecases.Produto.Cadastrar;

namespace Microsoft.Extensions.DependencyInjection;

public static class DominioDependencyConfig
{
    public static void AddDomainDependencies(this IServiceCollection services)
    {
        services.AddScoped<IProdutoCadastroDomainService, ProdutoCadastroDomainService>();
        services.AddScoped<IProdutoBuscaPorIdDomainService, ProdutoBuscaPorIdDomainService>();
        services.AddScoped<IProdutoListaDomainService, ProdutoListaDomainService>();
        services.AddScoped<IProdutoEdicaoDomainService, ProdutoEdicaoDomainService>();

        services.AddScoped<IFornecedorBuscaPorIdDomainService, FornecedorBuscaPorIdDomainService>();
    }
}