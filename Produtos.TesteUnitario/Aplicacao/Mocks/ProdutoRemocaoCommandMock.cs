using Aplicacao.CasosUso.Remocao;
using Bogus;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoRemocaoCommandMock : Faker<ProdutoRemocaoCommand>
{
    private ProdutoRemocaoCommandMock() : base("pt_BR")
    {
        RuleFor(produto => produto.Id, faker => faker.Lorem.Random.Int(min: 1));
    }

    public static ProdutoRemocaoCommand GerarObjeto()
    {
        return new ProdutoRemocaoCommandMock().Generate();
    }

    public static ProdutoRemocaoCommand? GerarObjetoNulo() => null;
}