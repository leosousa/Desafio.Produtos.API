using Aplicacao.CasosUso.Produto.Cadastrar;
using Bogus;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoCadastroCommandResultMock : Faker<ProdutoCadastroCommandResult>
{
    private ProdutoCadastroCommandResultMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Id, faker => faker.Lorem.Random.Int(min:1));
    }

    public static ProdutoCadastroCommandResult GerarObjeto()
    {
        return new ProdutoCadastroCommandResultMock().Generate();
    }
}