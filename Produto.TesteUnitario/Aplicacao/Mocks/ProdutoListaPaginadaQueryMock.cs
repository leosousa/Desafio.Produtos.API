using Aplicacao.CasosUso.ListaPaginada;
using Bogus;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoListaPaginadaQueryMock : Faker<ProdutoListaPaginadaQuery>
{
    private ProdutoListaPaginadaQueryMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min: 1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(produto => produto.Situacao, faker => faker.Random.Bool());

        RuleFor(produto => produto.Descricao, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(produto => produto.DataFabricacao, faker => faker.Date.Past());

        RuleFor(produto => produto.DataValidade, faker => faker.Date.Future());

        RuleFor(produto => produto.IdFornecedor, faker => faker.Random.Int(min: 1));
    }

    public static ProdutoListaPaginadaQuery GerarObjeto()
    {
        return new ProdutoListaPaginadaQueryMock().Generate();
    }

    public static ProdutoListaPaginadaQuery? GerarObjetoNulo() => null;
}