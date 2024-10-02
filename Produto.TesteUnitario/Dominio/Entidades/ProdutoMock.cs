using Bogus;
using Dominio.Entidades;

namespace TesteUnitario.Dominio;

public class ProdutoMock : Faker<Produto>
{
    private ProdutoMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Id, faker => faker.Random.Int(min: 1));

        RuleFor(produto => produto.Descricao, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(produto => produto.DataFabricacao, faker => faker.Date.Past());

        RuleFor(produto => produto.DataValidade, faker => faker.Date.Future());

        RuleFor(produto => produto.Situacao, faker => faker.Random.Bool());

        RuleFor(produto => produto.IdFornecedor, faker => faker.Random.Int(min: 1));
    }

    public static Produto GerarObjeto()
    {
        return new ProdutoMock().Generate();
    }

    public static Produto? GerarObjetoNulo()
    {
        return null;
    }
}