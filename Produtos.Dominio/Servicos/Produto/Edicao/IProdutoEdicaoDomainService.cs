using Flunt.Notifications;

namespace Dominio.Servicos.Produto.Editar;

public interface IProdutoEdicaoDomainService
{
    bool IsValid { get; }

    IReadOnlyCollection<Notification> Notifications { get; }

    Task<Entidades.Produto?> EditarAsync(Entidades.Produto produto, CancellationToken cancellationToken);
}