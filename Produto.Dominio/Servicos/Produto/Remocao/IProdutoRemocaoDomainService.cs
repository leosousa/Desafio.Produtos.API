using Flunt.Notifications;

namespace Dominio.Servicos.Produto.Remocao;

public interface IProdutoRemocaoDomainService
{
    bool IsValid { get; }

    IReadOnlyCollection<Notification> Notifications { get; }

    Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
}