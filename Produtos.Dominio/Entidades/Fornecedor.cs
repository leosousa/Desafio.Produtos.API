namespace Dominio.Entidades;

/// <summary>
/// Armazena um fornecedor de um produto
/// </summary>
public class Fornecedor : Entidade
{
    public string Descricao { get; private set; }

    public string Cnpj { get; private set; }

    public ICollection<Produto> Produtos { get; private set; }
}