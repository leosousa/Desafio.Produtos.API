using Aplicacao.CasosUso.Produto.Cadastrar;
using Bogus;
using Bogus.DataSets;
using Dominio.Entidades;
using System.Runtime.CompilerServices;

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

    public static Produto? GerarObjetoNulo() => null;

    public static Produto GerarObjetoInvalido()
    {
        var produto = GerarObjeto();

        produto.AtualizarDescricao(string.Empty);

        return produto;
    }

    public static Produto GerarObjetoValido()
    {
        var produto = new ProdutoMock().Generate();

        produto.AtualizarDataValidade(produto.DataFabricacao.AddDays(1));

        return produto;
    }

    public static Produto GerarObjetoSemFornecedor()
    {
        var produto = GerarObjeto();

        produto.AtualizarFornecedor(0);

        return produto;
    }

    public static IEnumerable<Produto> GerarObjetoLista(int quantidade = 10)
    {
        var lista = new List<Produto>();

        for (var index = 0; index < quantidade; index++)
        {
            lista.Add(GerarObjeto());
        }

        return lista;
    }
}