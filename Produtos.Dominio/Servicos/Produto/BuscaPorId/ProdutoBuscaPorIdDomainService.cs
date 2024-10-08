using Dominio.Contratos.Repositorio;
using Dominio.Entidades;
using Flunt.Notifications;

namespace Dominio.Servicos.Produto.BuscaPorId;

public class ProdutoBuscaPorIdDomainService : Notifiable<Notification>, IProdutoBuscaPorIdDomainService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoBuscaPorIdDomainService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Entidades.Produto?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            AddNotification(nameof(id), "Código do produto inválido");

            return await Task.FromResult<Entidades.Produto?>(null);
        }

        var produtoEncontrado = await _produtoRepository.BuscarPorIdAsync(id);

        if (produtoEncontrado is null)
        {
            AddNotification(nameof(Entidades.Produto), "Produto não encontrado");

            return await Task.FromResult<Entidades.Produto?>(null);
        }

        return await Task.FromResult(produtoEncontrado);
    }
}