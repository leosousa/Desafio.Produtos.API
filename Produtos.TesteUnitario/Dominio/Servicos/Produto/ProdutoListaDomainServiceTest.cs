using Dominio.Contratos.Repositorio;
using Dominio.Servicos.Produto.ListaPaginada;
using Moq;
using System.Linq.Expressions;
using TesteUnitario.Dominio.DTOs;
using ProdutoEntidade = Dominio.Entidades.Produto;

namespace TesteUnitario.Dominio.Servicos.Produto;

public class ProdutoListaDomainServiceTest
{
    private Mock<IProdutoRepository> _produtoRepository;

    public ProdutoListaDomainServiceTest()
    {
        _produtoRepository = new();
    }

    private ProdutoListaDomainService GerarCenario(Mock<IProdutoRepository> _produtoRepository)
    {
        return new ProdutoListaDomainService(_produtoRepository.Object);
    }

    [Fact(DisplayName = "Listar sem filtros selecionados")]
    public async Task ListarSemFiltrosSelecionados()
    {
        var filtros = ProdutoListaFiltroDTOMock.GerarObjetoNulo();
        var listaSemItens = new List<ProdutoEntidade>();
        var numeroItens = listaSemItens.Count();

        _produtoRepository.Setup(repository =>
            repository.CountAsync(It.IsAny<Expression<Func<ProdutoEntidade, bool>>>()))
        .ReturnsAsync(numeroItens);

        _produtoRepository.Setup(repository =>
            repository.ListarAsync(It.IsAny<Expression<Func<ProdutoEntidade, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaSemItens);

        var servicoDominio = GerarCenario(_produtoRepository);

        var result = await servicoDominio.ListarAsync(filtros!);

        Assert.NotNull(result);
        Assert.Empty(result.Itens!);
        Assert.Empty(servicoDominio.Notifications);
    }

    [Fact(DisplayName = "Listar com filtros selecionados")]
    public async Task ListarComFiltrosSelecionados()
    {
        var filtros = ProdutoListaFiltroDTOMock.GerarObjeto();
        var listaComItens = ProdutoMock.GerarObjetoLista(10);
        var numeroItens = listaComItens.Count();

        _produtoRepository.Setup(repository =>
            repository.CountAsync(It.IsAny<Expression<Func<ProdutoEntidade, bool>>>()))
        .ReturnsAsync(numeroItens);

        _produtoRepository.Setup(repository =>
            repository.ListarAsync(It.IsAny<Expression<Func<ProdutoEntidade, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
        .ReturnsAsync(listaComItens);

        var servicoDominio = GerarCenario(_produtoRepository);

        var result = await servicoDominio.ListarAsync(filtros!);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Itens!);
        Assert.Empty(servicoDominio.Notifications);
    }
}