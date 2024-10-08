using Aplicacao.CasosUso.BuscaPorId;
using Aplicacao.CasosUso.ListaPaginada;
using Bogus;
using Dominio.DTOs;
using Dominio.Entidades;
using System.Diagnostics;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoItemResultMock : Faker<ProdutoItemResult>
{
    private ProdutoItemResultMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(produto => produto.Descricao, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(produto => produto.DataFabricacao, faker => faker.Date.Past());

        RuleFor(produto => produto.DataValidade, faker => faker.Date.Future());

        RuleFor(produto => produto.Situacao, faker => faker.Random.Bool());
    }

    public static ProdutoItemResult GerarObjeto()
    {
        return new ProdutoItemResultMock().Generate();
    }

    public static IEnumerable<ProdutoItemResult> GerarObjetoLista()
    {
        return new List<ProdutoItemResult>()
        {
            GerarObjeto(),
            GerarObjeto(),
            GerarObjeto()
        };
    }
}