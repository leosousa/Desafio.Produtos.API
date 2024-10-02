using Dominio.Contratos.Repositorio;
using Dominio.Servicos.Produto.Cadastrar;
using Flunt.Notifications;

namespace Dominio.Usecases.Produto.Cadastrar;

public class ProdutoCadastroDomainService : Notifiable<Notification>, IProdutoCadastroDomainService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoCadastroDomainService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Entidades.Produto?> CadastrarAsync(Entidades.Produto produto, CancellationToken cancellationToken)
    {
        if (produto is null)
        {
            AddNotification(nameof(Entidades.Produto), "Produto não informado");

            return await Task.FromResult<Entidades.Produto?>(null);
        }

        produto.Validate();

        if (!produto.IsValid)
        {
            AddNotifications(produto);

            return await Task.FromResult<Entidades.Produto?>(null);
        }

        var produtoCadastrado = await _produtoRepository.CadastrarAsync(produto);

        return await Task.FromResult(produtoCadastrado);
    }
}