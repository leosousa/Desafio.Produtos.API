using Aplicacao.CasosUso.ListaPaginada;
using Bogus;
using Dominio.DTOs;
using Dominio.Entidades;
using TesteUnitario.Dominio.DTOs;
using TesteUnitario.Dominio;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoListaPaginadaQueryResultMock : Faker<ProdutoListaPaginadaQueryResult>
{
    private ProdutoListaPaginadaQueryResultMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(produto => produto.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(produto => produto.TotalRegistros, faker => faker.Random.Int(min: 1));

        RuleFor(produto => produto.Itens, ProdutoItemResultMock.GerarObjetoLista());
    }

    public static ProdutoListaPaginadaQueryResult GerarObjeto()
    {
        return new ProdutoListaPaginadaQueryResultMock().Generate();
    }

    public static ProdutoListaPaginadaQueryResult GerarObjetoSemItens()
    {
        return new ProdutoListaPaginadaQueryResult
        {
            Itens = new List<ProdutoItemResult>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}