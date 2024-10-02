using AutoMapper;
using Dominio.Entidades;
using Dominio.Servicos.Fornecedor.BuscaPorId;
using Dominio.Servicos.Produto.Cadastrar;
using Flunt.Notifications;
using MediatR;

namespace Aplicacao.CasosUso.Produto.Cadastrar;

public class ProdutoCadastroCommandHandler : Notifiable<Notification>, 
    IRequestHandler<ProdutoCadastroCommand, ProdutoCadastroCommandResult?>
{
    private readonly IProdutoCadastroDomainService _produtoCadastroService;
    private readonly IFornecedorBuscaPorIdDomainService _fornecedorBuscaPorIdService;
    private readonly IMapper _mapper;

    public ProdutoCadastroCommandHandler(
        IProdutoCadastroDomainService produtoCadastroService, 
        IFornecedorBuscaPorIdDomainService fornecedorBuscaPorIdService,
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

        if (fornecedor is null)
        {
            AddNotification(nameof(Fornecedor), "Fornecedor não encontrado!");

            return await Task.FromResult<ProdutoCadastroCommandResult?>(null);
        }

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