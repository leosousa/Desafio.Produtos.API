using Aplicacao.CasosUso.Edicao;
using Aplicacao.CasosUso.Produto.Cadastrar;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Servicos.Fornecedor.BuscaPorId;
using Dominio.Servicos.Produto.Editar;
using Moq;
using TesteUnitario.Aplicacao.Mocks;
using TesteUnitario.Dominio;
using TesteUnitario.Dominio.Entidades;

namespace TesteUnitario.Aplicacao.CasosUso;

public class ProdutoEdicaoCommandHandlerTest
{
    private readonly Mock<IProdutoEdicaoDomainService> _produtoEdicaoService;
    private readonly Mock<IFornecedorBuscaPorIdDomainService> _fornecedorBuscaPorIdService;
    private readonly Mock<IMapper> _mapper;

    public ProdutoEdicaoCommandHandlerTest()
    {
        _produtoEdicaoService = new();
        _fornecedorBuscaPorIdService = new();
        _mapper = new();
    }

    private ProdutoEdicaoCommandHandler GerarCenario()
    {
        return new ProdutoEdicaoCommandHandler(
            _produtoEdicaoService.Object,
            _fornecedorBuscaPorIdService.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Não deve editar o produto se o mesmo não for enviado")]
    public async Task NaoDeveEditarProdutoSeOMesmoNaoForEnviado()
    {
        var command = ProdutoEdicaoCommandMock.GerarObjetoNulo();

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o produto se o fornecedor não for encontrado")]
    public async Task NaoDeveEditarProdutoSeFornecedorNaoForEncontrado()
    {
        var command = ProdutoEdicaoCommandMock.GerarObjeto();
        var produto = ProdutoMock.GerarObjetoValido();
        var fornecedorNaoEncontrado = FornecedorMock.GerarObjetoNulo();

        _mapper.Setup(mapper => 
            mapper.Map<Produto>(It.IsAny<ProdutoEdicaoCommand>()))
        .Returns(produto);

        _fornecedorBuscaPorIdService.Setup(service =>
            service.BuscaPorId(command.IdFornecedor))
        .ReturnsAsync(fornecedorNaoEncontrado);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve editar o produto se o produto estiver inválido")]
    public async Task NaoDeveEditarProdutoSeOMesmoForInvalido()
    {
        var command = ProdutoEdicaoCommandMock.GerarObjeto();
        var produto = ProdutoMock.GerarObjetoValido();
        var fornecedorEncontrado = FornecedorMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<Produto>(It.IsAny<ProdutoEdicaoCommand>()))
        .Returns(produto);

        _fornecedorBuscaPorIdService.Setup(service =>
            service.BuscaPorId(command.IdFornecedor))
        .ReturnsAsync(fornecedorEncontrado);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Deve editar o produto se os dados forem válidos")]
    public async Task DeveEditarProdutoSeDadosForemValidos()
    {
        var command = ProdutoEdicaoCommandMock.GerarObjeto();
        var produto = ProdutoMock.GerarObjetoValido();
        var fornecedorEncontrado = FornecedorMock.GerarObjeto();
        var produtoEdicaoCommandResult = ProdutoEdicaoCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper =>
            mapper.Map<Produto>(It.IsAny<ProdutoEdicaoCommand>()))
        .Returns(produto);

        _fornecedorBuscaPorIdService.Setup(service =>
            service.BuscaPorId(It.IsAny<int>()))
        .ReturnsAsync(fornecedorEncontrado);

        _produtoEdicaoService.Setup(service =>
            service.EditarAsync(It.IsAny<Produto>(), CancellationToken.None))
        .ReturnsAsync(produto);

        _produtoEdicaoService.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper => 
            mapper.Map<ProdutoEdicaoCommandResult>(It.IsAny<Produto>()))
        .Returns(produtoEdicaoCommandResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}