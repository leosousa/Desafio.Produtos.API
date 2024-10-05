using Aplicacao.CasosUso.BuscaPorId;
using Aplicacao.CasosUso.Produto.Cadastrar;
using Bogus;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoBuscaPorIdQueryMock : Faker<ProdutoBuscaPorIdQuery>
{
    private ProdutoBuscaPorIdQueryMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Id, faker => faker.Lorem.Random.Int(min: 1));
    }

    public static ProdutoBuscaPorIdQuery? GerarObjetoNulo() => null;

    public static ProdutoBuscaPorIdQuery GerarObjeto()
    {
        return new ProdutoBuscaPorIdQueryMock().Generate();
    }
}