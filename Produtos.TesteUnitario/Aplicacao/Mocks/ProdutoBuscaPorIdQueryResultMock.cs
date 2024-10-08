using Aplicacao.CasosUso.BuscaPorId;
using Bogus;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoBuscaPorIdQueryResultMock : Faker<ProdutoBuscaPorIdQueryResult>
{
    private ProdutoBuscaPorIdQueryResultMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(produto => produto.Descricao, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(produto => produto.DataFabricacao, faker => faker.Date.Past());

        RuleFor(produto => produto.DataValidade, faker => faker.Date.Future());

        RuleFor(produto => produto.Situacao, faker => faker.Random.Bool());
    }

    public static ProdutoBuscaPorIdQueryResult GerarObjeto()
    {
        return new ProdutoBuscaPorIdQueryResultMock().Generate();
    }
}