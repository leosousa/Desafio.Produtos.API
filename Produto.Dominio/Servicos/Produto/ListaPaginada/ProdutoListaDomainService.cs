using Dominio.Contratos.Repositorio;
using Dominio.DTOs;
using Flunt.Notifications;
using LinqKit;

namespace Dominio.Servicos.Produto.ListaPaginada;

public class ProdutoListaDomainService : Notifiable<Notification>, IProdutoListaDomainService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoListaDomainService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<ListaPaginadaResultDTO<Entidades.Produto>> ListarAsync(ProdutoListaFiltroDTO filtros, int numeroPagina = 1, int tamanhoPagina = 10)
    {
        var predicado = PredicateBuilder.New<Entidades.Produto>(true);

        if (filtros is not null)
        {
            predicado = AdicionarFiltrosBuscaNaConsulta(filtros, predicado);
        }

        var quantidadeProdutos = await _produtoRepository.CountAsync(predicado);

        // O conceito de paginação para o negócio vai da página 1 até a página N
        if (numeroPagina <= 0)
        {
            // Com isso, a página informada menor ou igual a 0 setamos para 1
            numeroPagina = 1;
        }

        // Ao enviar para o repositório, trabalhamos com o conceito de página 0 até N,
        // para sabermos quantos registros pular internamente na busca, por isso enviamos
        // numeroPagina - 1
        var produtos = await _produtoRepository.ListarAsync(predicado, numeroPagina - 1, tamanhoPagina);

        var totalPaginas = quantidadeProdutos / tamanhoPagina;

        var result = new ListaPaginadaResultDTO<Entidades.Produto>
        {
            Itens = produtos,
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            TotalRegistros = quantidadeProdutos,
            TotalPaginas = (quantidadeProdutos % tamanhoPagina == 0) ? totalPaginas : totalPaginas + 1
        };

        return await Task.FromResult(result);
    }

    private static ExpressionStarter<Entidades.Produto> AdicionarFiltrosBuscaNaConsulta(ProdutoListaFiltroDTO filtros, ExpressionStarter<Entidades.Produto> predicado)
    {
        if (!string.IsNullOrEmpty(filtros.Descricao))
        {
            predicado = predicado.And(p => p.Descricao.Contains(filtros.Descricao));
        }

        if (filtros.Situacao is not null)
        {
            predicado = predicado.And(p => p.Situacao == filtros.Situacao.Value);
        }

        if (filtros.DataFabricacao is not null)
        {
            predicado = predicado.And(p => p.DataFabricacao >= filtros.DataFabricacao.Value);
        }

        if (filtros.DataValidade is not null)
        {
            predicado = predicado.And(p => p.DataValidade >= filtros.DataValidade.Value);
        }

        if (filtros.IdFornecedor is not null)
        {
            predicado = predicado.And(p => p.IdFornecedor == filtros.IdFornecedor.Value);
        }

        return predicado;
    }
}