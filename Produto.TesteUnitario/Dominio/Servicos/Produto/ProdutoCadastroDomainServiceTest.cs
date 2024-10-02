using Dominio.Contratos.Repositorio;
using Dominio.Servicos.Fornecedor.BuscaPorId;
using Dominio.Usecases.Produto.Cadastrar;
using Moq;

namespace TesteUnitario.Dominio.Servicos.Produto;

public class ProdutoCadastroDomainServiceTest
{
    private Mock<IProdutoRepository> _produtoRepository;

    public ProdutoCadastroDomainServiceTest()
    {
        _produtoRepository = new();
    }

    private ProdutoCadastroDomainService GerarCenario(Mock<IProdutoRepository> _produtoRepository)
    {
        return new ProdutoCadastroDomainService(_produtoRepository.Object);
    }

    [Fact(DisplayName = "Não deve cadastrar o produto se o produto não for enviado")]
    public async Task NaoDeveCadastrarProdutoSeOMesmoNaoForEnviado()
    {
        var produtoParaCadastrar = ProdutoMock.GerarObjetoNulo();

        var servicoDominio = GerarCenario(_produtoRepository);

        var result = await servicoDominio.CadastrarAsync(produtoParaCadastrar!, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve cadastrar o produto quando o produto for enviado")]
    public async Task DeveCadastrarProdutoSeOMesmoForEnviado()
    {
        var produtoParaCadastrar = ProdutoMock.GerarObjeto();
        var produtoCadastrado = produtoParaCadastrar;

        _produtoRepository.Setup(repository => repository.CadastrarAsync(produtoParaCadastrar)).ReturnsAsync(produtoCadastrado);

        var servicoDominio = GerarCenario(_produtoRepository);

        var result = await servicoDominio.CadastrarAsync(produtoParaCadastrar!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve cadastrar o produto se o produto não tiver descrição")]
    public async Task NaoDeveCadastrarProdutoSeNaoTiverDescricao()
    {
        var produtoParaCadastrar = ProdutoMock.GerarObjeto();
        produtoParaCadastrar.AtualizarDescricao(string.Empty);

        var produtoCadastrado = produtoParaCadastrar;

        _produtoRepository.Setup(repository => repository.CadastrarAsync(produtoParaCadastrar)).ReturnsAsync(produtoCadastrado);

        var servicoDominio = GerarCenario(_produtoRepository);

        var result = await servicoDominio.CadastrarAsync(produtoParaCadastrar!, CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve cadastrar o produto se a data de fabricação for maior do que a data de validade")]
    public async Task NaoDeveCadastrarProdutoSeDataFabricacaoMaiorQueDataValidade()
    {
        var produtoParaCadastrar = ProdutoMock.GerarObjeto();
        produtoParaCadastrar.AtualizarDataFabricacao(produtoParaCadastrar.DataValidade.AddDays(1));

        var produtoCadastrado = produtoParaCadastrar;

        _produtoRepository.Setup(repository => repository.CadastrarAsync(produtoParaCadastrar)).ReturnsAsync(produtoCadastrado);

        var servicoDominio = GerarCenario(_produtoRepository);

        var result = await servicoDominio.CadastrarAsync(produtoParaCadastrar!, CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(servicoDominio.Notifications);
    }
}