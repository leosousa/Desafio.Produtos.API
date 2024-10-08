using Flunt.Notifications;

namespace Aplicacao.CasosUso.ListaPaginada;

public class ProdutoListaPaginadaQueryResult : Notifiable<Notification>
{
    public IEnumerable<ProdutoItemResult>? Itens { get; init; }

    public int NumeroPagina { get; init; }

    public int TamanhoPagina { get; init; }

    public int TotalRegistros { get; init; }

    public int TotalPaginas { get; init; }
}

public class ProdutoItemResult
{
    /// <summary>
    /// Identificador do produto
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descrição do produto
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Situação de atividade do produto. True = Ativo. False = Inativo
    /// </summary>
    public bool Situacao { get; set; }

    /// <summary>
    /// Data de fabricação do produto
    /// </summary>
    public DateTime DataFabricacao { get; set; }

    /// <summary>
    /// Data de validade do produto
    /// </summary>
    public DateTime DataValidade { get; set; }
}