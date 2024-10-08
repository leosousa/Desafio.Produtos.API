using Dominio.Entidades;

namespace Dominio.DTOs;

public record ListaPaginadaResultDTO<T> where T : Entidade
{
    public IEnumerable<T>? Itens { get; init; }

    public int NumeroPagina { get; init; }

    public int TamanhoPagina { get; init; }

    public int TotalRegistros { get; init; }

    public int TotalPaginas { get; init; }
}