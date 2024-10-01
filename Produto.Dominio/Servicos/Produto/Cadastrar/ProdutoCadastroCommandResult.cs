namespace Dominio.Usecases.Produto.Cadastrar;

public record ProdutoCadastroCommandResult
{
    public Entidades.Produto ProdutoCadastrado { get; init; }

    public ProdutoCadastroCommandResult(Entidades.Produto produto)
    {
        ProdutoCadastrado = produto;
    }
}