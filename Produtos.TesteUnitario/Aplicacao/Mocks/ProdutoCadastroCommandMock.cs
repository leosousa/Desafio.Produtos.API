using Aplicacao.CasosUso.Produto.Cadastrar;
using Bogus;
using Dominio.Entidades;
using TesteUnitario.Dominio;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoCadastroCommandMock : Faker<ProdutoCadastroCommand>
{
    private ProdutoCadastroCommandMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Descricao, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(produto => produto.DataFabricacao, faker => faker.Date.Past());

        RuleFor(produto => produto.DataValidade, faker => faker.Date.Future());

        RuleFor(produto => produto.Situacao, faker => faker.Random.Bool());

        RuleFor(produto => produto.IdFornecedor, faker => faker.Random.Int(min: 1));
    }

    public static ProdutoCadastroCommand GerarObjeto()
    {
        return new ProdutoCadastroCommandMock().Generate();
    }

    public static ProdutoCadastroCommand GerarObjetoInvalido()
    {
        var command = GerarObjeto();

        command.Descricao = string.Empty;

        return command;
    }

    public static ProdutoCadastroCommand GerarObjetoSemFornecedor()
    {
        var command = GerarObjeto();

        command.IdFornecedor = 0;

        return command;
    }
}