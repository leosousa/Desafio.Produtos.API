namespace Dominio.Contratos.Servicos;

public interface IProdutoCadastroService
{
    Task<Entidades.Produto> CadastrarAsync(Entidades.Produto produto, CancellationToken cancellationToken);
}