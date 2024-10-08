using Aplicacao.CasosUso.BuscaPorId;
using Aplicacao.CasosUso.Produto.Cadastrar;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Servicos.Produto.BuscaPorId;
using Moq;
using TesteUnitario.Aplicacao.Mocks;
using TesteUnitario.Dominio;

namespace TesteUnitario.Aplicacao.CasosUso;

public class ProdutoBuscaPorIdQueryHandlerTest
{
    private readonly Mock<IProdutoBuscaPorIdDomainService> _produtoBuscaPorIdService;
    private readonly Mock<IMapper> _mapper;

    public ProdutoBuscaPorIdQueryHandlerTest()
    {
        _produtoBuscaPorIdService = new();
        _mapper = new();
    }

    private ProdutoBuscaPorIdQueryHandler GerarCenario()
    {
        return new ProdutoBuscaPorIdQueryHandler(
            _produtoBuscaPorIdService.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Não deve buscar se o identificador do produto não for informado")]
    public async Task NaoDeveBuscarProdutoSeIdentificadorNaoForInformado()
    {
        var query = ProdutoBuscaPorIdQueryMock.GerarObjetoNulo();
        var produto = ProdutoMock.GerarObjetoInvalido();

        _mapper.Setup(mapper => mapper.Map<Produto>(It.IsAny<ProdutoCadastroCommand>())).Returns(produto);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Notifications);
    }

    [Fact(DisplayName = "Buscar produto que está cadastrado")]
    public async Task BuscarProdutoQueEstaCadastrado()
    {
        var query = ProdutoBuscaPorIdQueryMock.GerarObjeto();
        var produto = ProdutoMock.GerarObjeto();
        var produtoBuscaPorIdQueryResult = ProdutoBuscaPorIdQueryResultMock.GerarObjeto();

        _produtoBuscaPorIdService.Setup(repository =>
            repository.BuscarPorIdAsync(query!.Id, CancellationToken.None))
        .ReturnsAsync(produto);

        _produtoBuscaPorIdService.SetupGet(property => property.IsValid).Returns(true);

        _mapper.Setup(mapper => mapper.Map<ProdutoBuscaPorIdQueryResult>(It.IsAny<Produto>()))
            .Returns(produtoBuscaPorIdQueryResult);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Notifications);
    }
}