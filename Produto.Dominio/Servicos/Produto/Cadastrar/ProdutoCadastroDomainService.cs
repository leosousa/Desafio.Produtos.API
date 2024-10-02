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

    public async Task<Entidades.Produto> CadastrarAsync(Entidades.Produto produto, CancellationToken cancellationToken)
    {
        produto.Validate();

        if (!produto.IsValid)
        {
            AddNotifications(produto);
        }

        var produtoCadastrado = await _produtoRepository.CadastrarAsync(produto);

        return await Task.FromResult(produtoCadastrado);
    }
}