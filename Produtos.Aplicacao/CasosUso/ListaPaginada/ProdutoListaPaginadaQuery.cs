using MediatR;

namespace Aplicacao.CasosUso.ListaPaginada;

public record ProdutoListaPaginadaQuery : IRequest<ProdutoListaPaginadaQueryResult>
{
    public string? Descricao { get; init; }
    public bool? Situacao { get; init; }
    public DateTime? DataFabricacao { get; init; }
    public DateTime? DataValidade { get; init; }
    public int? IdFornecedor { get; init; }
    public int NumeroPagina { get; init; }

    public int TamanhoPagina { get; init; }
}