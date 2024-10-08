namespace Dominio.Servicos.Produto.Cadastrar;

public interface IProdutoCadastroDomainService
{
    Task<Entidades.Produto?> CadastrarAsync(Entidades.Produto produto, CancellationToken cancellationToken);
}