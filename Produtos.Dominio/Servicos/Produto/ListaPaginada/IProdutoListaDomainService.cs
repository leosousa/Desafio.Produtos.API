using Dominio.DTOs;

namespace Dominio.Servicos.Produto.ListaPaginada;

public interface IProdutoListaDomainService
{
    Task<ListaPaginadaResultDTO<Entidades.Produto>> ListarAsync(ProdutoListaFiltroDTO filtros, int numeroPagina = 1, int tamanhoPagina = 10);
}