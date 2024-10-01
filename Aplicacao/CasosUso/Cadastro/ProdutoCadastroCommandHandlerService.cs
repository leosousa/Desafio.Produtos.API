using AutoMapper;
using Dominio.Contratos.Servicos;
using Dominio.Entidades;
using Dominio.Servicos.Fornecedor.BuscaPorId;
using Flunt.Notifications;
using MediatR;

namespace Aplicacao.CasosUso.Produto.Cadastrar;

public class ProdutoCadastroCommandHandlerService : Notifiable<Notification>, IRequestHandler<ProdutoCadastroCommand, ProdutoCadastroCommandResult?>
{
    private readonly IProdutoCadastroService _produtoCadastroService;
    private readonly IFornecedorBuscaPorIdService _fornecedorBuscaPorIdService;
    private readonly IMapper _mapper;

    public ProdutoCadastroCommandHandlerService(IProdutoCadastroService produtoCadastroService, 
        IFornecedorBuscaPorIdService fornecedorBuscaPorIdService,
        IMapper mapper)
    {
        _produtoCadastroService = produtoCadastroService;
        _fornecedorBuscaPorIdService = fornecedorBuscaPorIdService;
        _mapper = mapper;
    }

    public async Task<ProdutoCadastroCommandResult?> Handle(ProdutoCadastroCommand request, CancellationToken cancellationToken)
    {
        // Mapear entidade
        var produto = _mapper.Map<Dominio.Entidades.Produto>(request);

        if (!produto.IsValid)
        {
            AddNotifications(produto.Notifications);

            return await Task.FromResult<ProdutoCadastroCommandResult?>(null);
        }

        // Buscar fornecedor
        if (produto.IdFornecedor <= 0)
        {
            AddNotification(nameof(Fornecedor), "Fornecedor não informado!");

            return await Task.FromResult<ProdutoCadastroCommandResult?>(null);
        }

        var fornecedor = await _fornecedorBuscaPorIdService.BuscaPorId(produto.IdFornecedor);

        produto.AtualizarFornecedor(fornecedor.Id);

        // Cadastrar produto
        var produtoCadastrado = await _produtoCadastroService.CadastrarAsync(produto, cancellationToken);

        if (!produtoCadastrado.IsValid)
        {
            AddNotifications(produtoCadastrado.Notifications);

            return await Task.FromResult<ProdutoCadastroCommandResult?>(null);
        }

        return await Task.FromResult(new ProdutoCadastroCommandResult(produtoCadastrado.Id));
    }
}