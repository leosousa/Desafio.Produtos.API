using Dominio.Contratos.Repositorio;
using Flunt.Notifications;

namespace Dominio.Servicos.Produto.Remocao;

public class ProdutoRemocaoDomainService : Notifiable<Notification>, IProdutoRemocaoDomainService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoRemocaoDomainService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            AddNotification(nameof(id), Mensagens.Produto.IdProdutoNaoInformado);

            return await Task.FromResult(false);
        }

        var produto = await _produtoRepository.BuscarPorIdAsync(id);

        if (produto is null)
        {
            AddNotification(nameof(Entidades.Produto), Mensagens.Produto.ProdutoNaoEncontrado);

            return await Task.FromResult(false);
        }

        // Exclusão lógica
        produto.AtualizarSituacao(false);

        var produtoRemovido = await _produtoRepository.EditarAsync(produto);

        if (produtoRemovido is null)
        {
            AddNotification(nameof(Entidades.Produto), "Produto não removido");

            return await Task.FromResult(false);
        }

        return await Task.FromResult(produtoRemovido.Situacao == false);
    }
}