using Aplicacao.CasosUso.Edicao;
using Aplicacao.CasosUso.Produto.Cadastrar;
using Bogus;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoEdicaoCommandResultMock : Faker<ProdutoEdicaoCommandResult>
{
    private ProdutoEdicaoCommandResultMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Id, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(produto => produto.Descricao, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(produto => produto.DataFabricacao, faker => faker.Date.Past());

        RuleFor(produto => produto.DataValidade, faker => faker.Date.Future());

        RuleFor(produto => produto.Situacao, faker => faker.Random.Bool());

        RuleFor(produto => produto.IdFornecedor, faker => faker.Random.Int(min: 1));
    }

    public static ProdutoEdicaoCommandResult GerarObjeto()
    {
        return new ProdutoEdicaoCommandResultMock().Generate();
    }
}