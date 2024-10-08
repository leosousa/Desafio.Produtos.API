using Aplicacao.CasosUso.Edicao;
using Aplicacao.CasosUso.Remocao;
using Dominio;
using Dominio.Entidades;
using Dominio.Servicos.Produto.Remocao;
using Moq;
using TesteUnitario.Aplicacao.Mocks;

namespace TesteUnitario.Aplicacao.CasosUso;

public class ProdutoRemocaoCommandHandlerTest
{
    private readonly Mock<IProdutoRemocaoDomainService> _produtoRemocaoService;

    public ProdutoRemocaoCommandHandlerTest()
    {
        _produtoRemocaoService = new();
    }

    private ProdutoRemocaoCommandHandler GerarCenario()
    {
        return new ProdutoRemocaoCommandHandler(
            _produtoRemocaoService.Object
        );
    }

    [Fact(DisplayName = "Não deve remover o produto se o mesmo não for enviado")]
    public async Task NaoDeveRemoverProdutoSeOMesmoNaoForEnviado()
    {
        var command = ProdutoRemocaoCommandMock.GerarObjetoNulo();

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
        Assert.Contains(result.Notifications, notification => notification.Message == Mensagens.Produto.IdProdutoNaoInformado);
    }

    [Fact(DisplayName = "Não deve remover o produto quando o mesmo não for encontrado")]
    public async Task NaoDeveRemoverProdutoQuandoOMesmoNaoForEncontrado()
    {
        var command = ProdutoRemocaoCommandMock.GerarObjeto();
        var notificacoes = NotificationMock.GerarObjetoLista(nameof(Produto), Mensagens.Produto.ProdutoNaoEncontrado);

        _produtoRemocaoService.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(false);

        _produtoRemocaoService.SetupGet(property => property.IsValid).Returns(false);
        _produtoRemocaoService.SetupGet(property => property.Notifications).Returns(notificacoes);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
        Assert.Contains(result.Notifications, notification => notification.Message == Mensagens.Produto.ProdutoNaoEncontrado);
    }

    [Fact(DisplayName = "Erro ao remover o produto")]
    public async Task ErroRemoverProduto()
    {
        var command = ProdutoRemocaoCommandMock.GerarObjeto();
        var notificacoes = NotificationMock.GerarObjetoLista(nameof(Produto), Mensagens.Produto.ProdutoNaoRemovido);

        _produtoRemocaoService.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(false);

        _produtoRemocaoService.SetupGet(property => property.IsValid).Returns(true);
        _produtoRemocaoService.SetupGet(property => property.Notifications).Returns(notificacoes);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
        Assert.Contains(result.Notifications, notification => notification.Message == Mensagens.Produto.ProdutoNaoRemovido);
    }


    [Fact(DisplayName = "Remover o produto com sucesso")]
    public async Task RemoverProdutoComSucesso()
    {
        var command = ProdutoRemocaoCommandMock.GerarObjeto();

        _produtoRemocaoService.Setup(service =>
            service.RemoverAsync(It.IsAny<int>(), CancellationToken.None))
        .ReturnsAsync(true);

        _produtoRemocaoService.SetupGet(property => property.IsValid).Returns(true);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}