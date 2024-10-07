using Bogus;
using Dominio.Contratos.Repositorio;
using Dominio.Servicos.Produto.Remocao;
using Moq;
using ProdutoEntidade = Dominio.Entidades.Produto;

namespace TesteUnitario.Dominio.Servicos.Produto;

public class ProdutoRemocaoDomainServiceTest
{
    private Mock<IProdutoRepository> _produtoRepository;

    public ProdutoRemocaoDomainServiceTest()
    {
        _produtoRepository = new();
    }

    private ProdutoRemocaoDomainService GerarCenario()
    {
        return new ProdutoRemocaoDomainService(_produtoRepository.Object);
    }

    [Fact(DisplayName = "Não deve remover se o id do produto estiver inválido")]
    public async Task NaoDeveRemoverSeIdProdutoEstiverInvalido()
    {
        var id = 0;

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.RemoverAsync(id, CancellationToken.None);

        Assert.False(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve remover se o produto não for encontrado")]
    public async Task NaoDeveRemoverSeProdutoNaoForEncontrado()
    {
        var id = new Faker().Random.Int(min: 1);
        var produtoNaoEncontrado = ProdutoMock.GerarObjetoNulo();

        _produtoRepository.Setup(repository =>
            repository.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(produtoNaoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.RemoverAsync(id, CancellationToken.None);

        Assert.False(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Erro ao remover produto")]
    public async Task ErroAoRemoverProduto()
    {
        var id = new Faker().Random.Int(min: 1);
        var produtoEncontrado = ProdutoMock.GerarObjeto();
        var produtoNaoRemovido = ProdutoMock.GerarObjetoNulo();

        _produtoRepository.Setup(repository =>
            repository.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(produtoEncontrado);

        _produtoRepository.Setup(repository =>
            repository.EditarAsync(It.IsAny<ProdutoEntidade>()))
        .ReturnsAsync(produtoNaoRemovido!);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.RemoverAsync(id, CancellationToken.None);

        Assert.False(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve remover produto quando for encontrado")]
    public async Task DeveRemoverProdutoQuandoForEncontrado()
    {
        var id = new Faker().Random.Int(min: 1);
        var produtoEncontrado = ProdutoMock.GerarObjeto();
        var produtoRemovido = ProdutoMock.GerarObjetoRemovido();

        _produtoRepository.Setup(repository =>
            repository.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(produtoEncontrado);

        _produtoRepository.Setup(repository =>
            repository.EditarAsync(It.IsAny<ProdutoEntidade>()))
        .ReturnsAsync(produtoRemovido);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.RemoverAsync(id, CancellationToken.None);

        Assert.True(result);
        Assert.Empty(servicoDominio.Notifications);
    }
}