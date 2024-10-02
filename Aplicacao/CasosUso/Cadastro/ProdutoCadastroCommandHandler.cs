using AutoMapper;
using Dominio.Entidades;
using Dominio.Servicos.Fornecedor.BuscaPorId;
using Dominio.Servicos.Produto.Cadastrar;
using MediatR;

namespace Aplicacao.CasosUso.Produto.Cadastrar;

public class ProdutoCadastroCommandHandler : 
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
        ProdutoCadastroCommandResult result = new(); 

        if (!produto.IsValid)
        {
            result.AddNotifications(produto.Notifications);

            return await Task.FromResult(result);
        }

        // Buscar fornecedor
        var fornecedor = await _fornecedorBuscaPorIdService.BuscaPorId(produto.IdFornecedor);

        if (fornecedor is null)
        {
            result.AddNotification(nameof(Fornecedor), "Fornecedor não encontrado!");

            return await Task.FromResult(result);
        }

        produto.AtualizarFornecedor(fornecedor.Id);

        // Cadastrar produto
        var produtoCadastrado = await _produtoCadastroService.CadastrarAsync(produto, cancellationToken);

        if (produtoCadastrado is null)
        {
            result.AddNotification(nameof(Produto), "Produto não cadastrado!");

            return await Task.FromResult(result);
        }

        if (!produtoCadastrado.IsValid)
        {
            result.AddNotifications(produtoCadastrado.Notifications);

            return await Task.FromResult(result);
        }

        result = _mapper.Map<ProdutoCadastroCommandResult>(produto);

        return await Task.FromResult(result);
    }
}