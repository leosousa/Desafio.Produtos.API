using Dominio.Regras;

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

    public Fornecedor Fornecedor { get; private set; }

    public void Validate()
    {
        AddNotifications(new ProdutoRegraNegocio()
            .DataFabricacaoNaoPodeSerMaiorQueDataValidade(DataFabricacao, DataValidade)
            .DescricaoNaoPodeSerNula(Descricao)
            .FornecedorPrecisaSerInformado(IdFornecedor)
        );
    }

    public void AtualizarFornecedor(int idFornecedor)
    {
        IdFornecedor = idFornecedor;
    }

    public void AtualizarDescricao(string descricao)
    {
        Descricao = descricao;
    }

    public void AtualizarDataFabricacao(DateTime dataFabricacao)
    {
        DataFabricacao = dataFabricacao;
    }
}