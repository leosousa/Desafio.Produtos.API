using Bogus;
using Dominio.DTOs;
using Dominio.Entidades;

namespace TesteUnitario.Dominio.DTOs;

public class ProdutoListaPaginadaResultDTOMock : Faker<ListaPaginadaResultDTO<Produto>>
{
    private ProdutoListaPaginadaResultDTOMock() : base("pt_BR")
    {
        RuleFor(lista => lista.TamanhoPagina, faker => faker.Random.Int(min:1));

        RuleFor(lista => lista.NumeroPagina, faker => faker.Lorem.Random.Int(min: 1));

        RuleFor(produto => produto.TotalPaginas, faker => faker.Random.Int(min: 1));

        RuleFor(produto => produto.TotalRegistros, faker => faker.Random.Int(min: 1));

        RuleFor(produto => produto.Itens, ProdutoMock.GerarObjetoLista());
    }

    public static ListaPaginadaResultDTO<Produto> GerarObjeto()
    {
        return new ProdutoListaPaginadaResultDTOMock().Generate();
    }

    public static ListaPaginadaResultDTO<Produto> GerarListaSemItens()
    {
        return new ListaPaginadaResultDTO<Produto>
        {
            Itens = new List<Produto>(),
            NumeroPagina = 1,
            TamanhoPagina = 10,
            TotalPaginas = 1,
            TotalRegistros = 0
        };
    }
}