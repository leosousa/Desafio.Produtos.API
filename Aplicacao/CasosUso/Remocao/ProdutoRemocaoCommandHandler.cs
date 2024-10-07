using Dominio;
using Dominio.Servicos.Produto.Remocao;
using Flunt.Notifications;
using MediatR;

namespace Aplicacao.CasosUso.Remocao;

public class ProdutoRemocaoCommandHandler : Notifiable<Notification>, 
    IRequestHandler<ProdutoRemocaoCommand, ProdutoRemocaoCommandResult?>
{
    private readonly IProdutoRemocaoDomainService _produtoRemocaoService;

    public ProdutoRemocaoCommandHandler(IProdutoRemocaoDomainService produtoRemocaoService)
    {
        _produtoRemocaoService = produtoRemocaoService;
    }

    public async Task<ProdutoRemocaoCommandResult?> Handle(ProdutoRemocaoCommand request, CancellationToken cancellationToken)
    {
        ProdutoRemocaoCommandResult result = new();

        if (request is null)
        {
            result.AddNotification(nameof(Produto), Mensagens.Produto.IdProdutoNaoInformado);

            return await Task.FromResult(result);
        }

        var produtoRemovido = await _produtoRemocaoService.RemoverAsync(request.Id, cancellationToken);

        if (!_produtoRemocaoService.IsValid)
        {
            result.AddNotifications(_produtoRemocaoService.Notifications);

            return await Task.FromResult(result);
        }

        if (!produtoRemovido)
        {
            result.AddNotification(nameof(Produto), Mensagens.Produto.ProdutoNaoRemovido);

            return await Task.FromResult(result);
        }

        return await Task.FromResult(result);
    }
}