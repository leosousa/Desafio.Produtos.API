using Bogus;
using Dominio.Servicos.Produto.ListaPaginada;
namespace TesteUnitario.Dominio.DTOs;

public class ProdutoListaFiltroDTOMock : Faker<ProdutoListaFiltroDTO>
{
    private ProdutoListaFiltroDTOMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Situacao, faker => faker.Random.Bool());

        RuleFor(produto => produto.Descricao, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(produto => produto.DataFabricacao, faker => faker.Date.Past());

        RuleFor(produto => produto.DataValidade, faker => faker.Date.Future());

        RuleFor(produto => produto.IdFornecedor, faker => faker.Random.Int(min: 1));
    }

    public static ProdutoListaFiltroDTO GerarObjeto()
    {
        return new ProdutoListaFiltroDTOMock().Generate();
    }

    public static ProdutoListaFiltroDTO? GerarObjetoNulo() => null;
}