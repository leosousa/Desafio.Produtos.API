using Bogus;
using Dominio.Contratos.Repositorio;
using Dominio.Servicos.Produto.BuscaPorId;
using Moq;

namespace TesteUnitario.Dominio.Servicos.Produto;

public class ProdutoBuscaPorIdDomainServiceTest
{
    private Mock<IProdutoRepository> _produtoRepository;

    public ProdutoBuscaPorIdDomainServiceTest()
    {
        _produtoRepository = new();
    }

    private ProdutoBuscaPorIdDomainService GerarCenario(Mock<IProdutoRepository> _produtoRepository)
    {
        return new ProdutoBuscaPorIdDomainService(_produtoRepository.Object);
    }

    [Fact(DisplayName = "Não deve buscar se o id do produto estiver inválido")]
    public async Task NaoDeveBuscarSeIdProdutoEstiverInvalido()
    {
        var id = 0;

        var servicoDominio = GerarCenario(_produtoRepository);

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Buscar produto que não está cadastrado")]
    public async Task BuscarProdutoQueNaoEstaCadastrado()
    {
        var id = new Faker().Random.Int(min:1);
        var produtoNaoEncontrado = ProdutoMock.GerarObjetoNulo();

        _produtoRepository.Setup(repository => 
            repository.BuscarPorIdAsync(id))
        .ReturnsAsync(produtoNaoEncontrado);

        var servicoDominio = GerarCenario(_produtoRepository);

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Buscar produto que está cadastrado")]
    public async Task BuscarProdutoQueEstaCadastrado()
    {
        var id = new Faker().Random.Int(min: 1);
        var produtoEncontrado = ProdutoMock.GerarObjeto();

        _produtoRepository.Setup(repository =>
            repository.BuscarPorIdAsync(id))
        .ReturnsAsync(produtoEncontrado);

        var servicoDominio = GerarCenario(_produtoRepository);

        var result = await servicoDominio.BuscarPorIdAsync(id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(servicoDominio.Notifications);
    }
}