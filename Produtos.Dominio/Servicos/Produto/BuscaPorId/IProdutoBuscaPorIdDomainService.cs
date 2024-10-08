using Flunt.Notifications;

namespace Dominio.Servicos.Produto.BuscaPorId;

public interface IProdutoBuscaPorIdDomainService
{
    bool IsValid { get; }

    IReadOnlyCollection<Notification> Notifications { get; }

    Task<Entidades.Produto?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
}