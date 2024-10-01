namespace Dominio.Entidades;

/// <summary>
/// Armazena um produto
/// </summary>
public class Produto : Entidade
{
    /// <summary>
    /// Descrição do produto
    /// </summary>
    public string Descricao { get; private set; }

    /// <summary>
    /// Situação de atividade do produto. True = Ativo. False = Inativo
    /// </summary>
    public bool Situacao { get; private set; }

    /// <summary>
    /// Data de fabricação do produto
    /// </summary>
    public DateTime DataFabricacao { get; private set; }

    /// <summary>
    /// Data de validade do produto
    /// </summary>
    public DateTime DataValidade { get; private set; }

    /// <summary>
    /// Identificador do fornecedor
    /// </summary>
    public int IdFornecedor { get; private set; }
}