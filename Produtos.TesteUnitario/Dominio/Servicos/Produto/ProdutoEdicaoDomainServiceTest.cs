using Dominio.Contratos.Repositorio;
using Dominio.Servicos.Produto.Editar;
using Moq;
using TesteUnitario.Dominio.Entidades;

namespace TesteUnitario.Dominio.Servicos.Produto;

public class ProdutoEdicaoDomainServiceTest
{
    private Mock<IProdutoRepository> _produtoRepository;

    public ProdutoEdicaoDomainServiceTest()
    {
        _produtoRepository = new();
    }

    private ProdutoEdicaoDomainService GerarCenario()
    {
        return new ProdutoEdicaoDomainService(_produtoRepository.Object);
    }

    [Fact(DisplayName = "Não deve editar o produto se o produto não for enviado")]
    public async Task NaoDeveEditarProdutoSeOMesmoNaoForEnviado()
    {
        var produtoParaAlterar = ProdutoMock.GerarObjetoNulo();

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(produtoParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o produto se o produto não for encontrado")]
    public async Task NaoDeveEditarProdutoSeOMesmoNaoForEncontrado()
    {
        var produtoParaAlterar = ProdutoMock.GerarObjeto();
        var produtoNaoEncontrado = ProdutoMock.GerarObjetoNulo();

        _produtoRepository.Setup(repositorio => 
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(produtoNaoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(produtoParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }


    [Fact(DisplayName = "Deve editar o produto se dados inválidos")]
    public async Task DeveEditarProdutoSeDadosInvalidos()
    {
        var produtoParaAlterar = ProdutoMock.GerarObjetoInvalido();
        var produtoEncontrado = ProdutoMock.GerarObjetoValido();
        var fornecedorEncontrado = FornecedorMock.GerarObjeto();

        _produtoRepository.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(produtoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(produtoParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Deve editar o produto se dados informados estão corretos")]
    public async Task DeveEditarProdutoSeDadosEstaoCorretos()
    {
        var produtoParaAlterar = ProdutoMock.GerarObjetoValido();
        var produtoEncontrado = ProdutoMock.GerarObjetoValido();
        var fornecedorEncontrado = FornecedorMock.GerarObjeto();

        _produtoRepository.Setup(repositorio =>
            repositorio.BuscarPorIdAsync(It.IsAny<int>()))
        .ReturnsAsync(produtoEncontrado);

        var servicoDominio = GerarCenario();

        var result = await servicoDominio.EditarAsync(produtoParaAlterar!, CancellationToken.None);

        Assert.Null(result);
        Assert.NotNull(servicoDominio.Notifications);
    }
}