using Aplicacao.CasosUso.Produto.Cadastrar;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Servicos.Fornecedor.BuscaPorId;
using Dominio.Servicos.Produto.Cadastrar;
using Moq;
using TesteUnitario.Aplicacao.Mocks;
using TesteUnitario.Dominio;
using TesteUnitario.Dominio.Entidades;

namespace TesteUnitario.Aplicacao.CasosUso;

public class ProdutoCadastroCommandHandlerTest
{
    private readonly Mock<IProdutoCadastroDomainService> _produtoCadastroService;
    private readonly Mock<IFornecedorBuscaPorIdDomainService> _fornecedorBuscaPorIdService;
    private readonly Mock<IMapper> _mapper;

    public ProdutoCadastroCommandHandlerTest()
    {
        _produtoCadastroService = new();
        _fornecedorBuscaPorIdService = new();
        _mapper = new();
    }

    private ProdutoCadastroCommandHandler GerarCenario()
    {
        return new ProdutoCadastroCommandHandler(
            _produtoCadastroService.Object,
            _fornecedorBuscaPorIdService.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Não deve cadastrar o produto se o mesmo estiver inválido")]
    public async Task NaoDeveCadastrarProdutoSeOMesmoEstiverInvalido()
    {
        var command = ProdutoCadastroCommandMock.GerarObjetoInvalido();
        var produto = ProdutoMock.GerarObjetoInvalido();

        _mapper.Setup(mapper => mapper.Map<Produto>(It.IsAny<ProdutoCadastroCommand>())).Returns(produto);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Não deve cadastrar o produto se o fornecedor não for encontrado")]
    public async Task NaoDeveCadastrarProdutoSeOFornecedorNaoForEncontrado()
    {
        var command = ProdutoCadastroCommandMock.GerarObjeto();
        var produto = ProdutoMock.GerarObjeto();
        var fornecedor = FornecedorMock.GerarObjetoNulo();

        _mapper.Setup(mapper => mapper.Map<Produto>(It.IsAny<ProdutoCadastroCommand>()))
            .Returns(produto);

        _fornecedorBuscaPorIdService.Setup(fornecedorBuscaPorIdService =>
            fornecedorBuscaPorIdService.BuscaPorId(It.IsAny<int>()))
        .ReturnsAsync(fornecedor);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Erro ao cadastrar o produto")]
    public async Task ErroAoCadastrarProduto()
    {
        var command = ProdutoCadastroCommandMock.GerarObjeto();
        var produto = ProdutoMock.GerarObjeto();
        var produtoCadastrado = ProdutoMock.GerarObjetoNulo();
        var fornecedor = FornecedorMock.GerarObjeto();

        _mapper.Setup(mapper => mapper.Map<Produto>(It.IsAny<ProdutoCadastroCommand>()))
            .Returns(produto);

        _fornecedorBuscaPorIdService.Setup(fornecedorBuscaPorIdService =>
            fornecedorBuscaPorIdService.BuscaPorId(It.IsAny<int>()))
        .ReturnsAsync(fornecedor);

        _produtoCadastroService.Setup(produtoCadastroService =>
            produtoCadastroService.CadastrarAsync(It.IsAny<Produto>(), CancellationToken.None))
        .ReturnsAsync(produtoCadastrado);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Cadastrar produto com sucesso")]
    public async Task CadastrarProdutoComSucesso()
    {
        var command = ProdutoCadastroCommandMock.GerarObjeto();
        var produto = ProdutoMock.GerarObjeto();
        var produtoCadastrado = produto;
        var fornecedor = FornecedorMock.GerarObjeto();
        var produtoCadastradoCommandResult = ProdutoCadastroCommandResultMock.GerarObjeto();

        _mapper.Setup(mapper => mapper.Map<Produto>(It.IsAny<ProdutoCadastroCommand>()))
            .Returns(produto);

        _mapper.Setup(mapper => mapper.Map<ProdutoCadastroCommandResult>(It.IsAny<Produto>()))
            .Returns(produtoCadastradoCommandResult);

        _fornecedorBuscaPorIdService.Setup(fornecedorBuscaPorIdService =>
            fornecedorBuscaPorIdService.BuscaPorId(It.IsAny<int>()))
        .ReturnsAsync(fornecedor);

        _produtoCadastroService.Setup(produtoCadastroService =>
            produtoCadastroService.CadastrarAsync(It.IsAny<Produto>(), CancellationToken.None))
        .ReturnsAsync(produtoCadastrado);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(command!, CancellationToken.None);

        Assert.NotNull(produtoCadastradoCommandResult);
        Assert.Empty(produtoCadastradoCommandResult.Notifications);
        Assert.True(produtoCadastrado.IsValid);
        Assert.True(produtoCadastrado.Id > 0);
    }
}