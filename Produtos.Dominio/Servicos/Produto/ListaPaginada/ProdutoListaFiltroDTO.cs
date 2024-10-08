using Dominio.DTOs;

namespace Dominio.Servicos.Produto.ListaPaginada;

public class ProdutoListaFiltroDTO
{
    public string? Descricao { get; set; }
    public bool? Situacao { get; set; }
    public DateTime? DataFabricacao { get; set; }
    public DateTime? DataValidade { get; set; }
    public int? IdFornecedor { get; set; }
}