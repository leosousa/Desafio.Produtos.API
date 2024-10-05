using Dominio.Contratos.Repositorio;
using Flunt.Notifications;

namespace Dominio.Servicos.Produto.Editar;

public class ProdutoEdicaoDomainService : Notifiable<Notification>, IProdutoEdicaoDomainService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoEdicaoDomainService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Entidades.Produto?> EditarAsync(Entidades.Produto produto, CancellationToken cancellationToken)
    {
        if (produto is null)
        {
            AddNotification(nameof(Entidades.Produto), "Produto não informado");

            return await Task.FromResult<Entidades.Produto?>(null);
        }

        var produtoEncontrado = await _produtoRepository.BuscarPorIdAsync(produto.Id);

        if (produtoEncontrado is null)
        {
            AddNotification(nameof(Entidades.Produto), "Produto não encontrado");

            return await Task.FromResult<Entidades.Produto?>(null);
        }

        produtoEncontrado.AtualizarDescricao(produto.Descricao);
        produtoEncontrado.AtualizarDataFabricacao(produto.DataFabricacao);
        produtoEncontrado.AtualizarDataValidade(produto.DataValidade);
        produtoEncontrado.AtualizarSituacao(produto.Situacao);
        produtoEncontrado.AtualizarFornecedor(produto.Fornecedor);

        produto.Validate();

        if (!produto.IsValid)
        {
            AddNotifications(produto);

            return await Task.FromResult<Entidades.Produto?>(null);
        }

        var produtoEditado = await _produtoRepository.EditarAsync(produto);

        return await Task.FromResult(produtoEditado);
    }
}