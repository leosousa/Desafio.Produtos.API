using MediatR;
using System.Text.Json.Serialization;

namespace Aplicacao.CasosUso.Edicao;

public record ProdutoEdicaoCommand : IRequest<ProdutoEdicaoCommandResult>
{
    /// <summary>
    /// Identificador do produto
    /// </summary>
    [JsonIgnore]
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
    /// Identificador do fornecedor
    /// </summary>
    public int IdFornecedor { get; set; }
}