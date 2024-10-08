using Flunt.Notifications;

namespace Aplicacao.CasosUso.BuscaPorId;

public class ProdutoBuscaPorIdQueryResult : Notifiable<Notification>
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

    /// <summary>
    /// Fornecedor do produto
    /// </summary>
    public ProdutoBuscaPorIdFornecedorResult Fornecedor { get; set; }
}