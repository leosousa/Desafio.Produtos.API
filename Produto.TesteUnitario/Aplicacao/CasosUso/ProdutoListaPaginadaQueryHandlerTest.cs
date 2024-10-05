using Aplicacao.CasosUso.ListaPaginada;
using Aplicacao.CasosUso.Produto.Cadastrar;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Entidades;
using Dominio.Servicos.Produto.ListaPaginada;
using Moq;
using TesteUnitario.Aplicacao.Mocks;
using TesteUnitario.Dominio.DTOs;

namespace TesteUnitario.Aplicacao.CasosUso;

public class ProdutoListaPaginadaQueryHandlerTest
{
    private readonly Mock<IProdutoListaDomainService> _produtoListaDomainService;
    private readonly Mock<IMapper> _mapper;

    public ProdutoListaPaginadaQueryHandlerTest()
    {
        _produtoListaDomainService = new();
        _mapper = new();
    }

    private ProdutoListaPaginadaQueryHandler GerarCenario()
    {
        return new ProdutoListaPaginadaQueryHandler(
            _produtoListaDomainService.Object,
            _mapper.Object
        );
    }

    [Fact(DisplayName = "Listar sem filtros selecionados")]
    public async Task ListarSemFiltrosSelecionados()
    {
        var query = ProdutoListaPaginadaQueryMock.GerarObjetoNulo();
        var filtros = ProdutoListaFiltroDTOMock.GerarObjetoNulo();
        var listaPaginada = ProdutoListaPaginadaResultDTOMock.GerarObjeto();
        var queryResult = ProdutoListaPaginadaQueryResultMock.GerarObjeto();

        _mapper.Setup(mapper => 
            mapper.Map<ProdutoListaFiltroDTO>(It.IsAny<ProdutoListaPaginadaQuery>()))
        .Returns(filtros!);

        _produtoListaDomainService.Setup(produtoListaDomainService =>
            produtoListaDomainService.ListarAsync(It.IsAny<ProdutoListaFiltroDTO>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaPaginada);

        _mapper.Setup(mapper =>
            mapper.Map<ProdutoListaPaginadaQueryResult>(It.IsAny<ListaPaginadaResultDTO<Produto>>()))
        .Returns(queryResult!);

        var casoUso = GerarCenario();

        var result = await casoUso.Handle(query!, CancellationToken.None);

        Assert.NotNull(queryResult);
        Assert.NotEmpty(queryResult.Itens!);
    }
}