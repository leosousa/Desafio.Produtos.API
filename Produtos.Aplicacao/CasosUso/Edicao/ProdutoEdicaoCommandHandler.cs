using Aplicacao.CasosUso.Produto.Cadastrar;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Servicos.Fornecedor.BuscaPorId;
using Dominio.Servicos.Produto.Editar;
using MediatR;

namespace Aplicacao.CasosUso.Edicao;

public class ProdutoEdicaoCommandHandler :
    IRequestHandler<ProdutoEdicaoCommand, ProdutoEdicaoCommandResult?>
{
    private readonly IProdutoEdicaoDomainService _produtoEdicaoDomainService;
    private readonly IFornecedorBuscaPorIdDomainService _fornecedorBuscaPorIdService;
    private readonly IMapper _mapper;

    public ProdutoEdicaoCommandHandler(
        IProdutoEdicaoDomainService produtoEdicaoDomainService, 
        IFornecedorBuscaPorIdDomainService fornecedorBuscaPorIdService, 
        IMapper mapper)
    {
        _produtoEdicaoDomainService = produtoEdicaoDomainService;
        _fornecedorBuscaPorIdService = fornecedorBuscaPorIdService;
        _mapper = mapper;
    }

    public async Task<ProdutoEdicaoCommandResult?> Handle(ProdutoEdicaoCommand request, CancellationToken cancellationToken)
    {
        ProdutoEdicaoCommandResult result = new();

        if (request is null)
        {
            result.AddNotification(nameof(Produto), "Produto não informado");

            return await Task.FromResult(result);
        }

        var produto = _mapper.Map<Dominio.Entidades.Produto>(request);

        // Buscar fornecedor
        var fornecedor = await _fornecedorBuscaPorIdService.BuscaPorId(produto.IdFornecedor);

        if (fornecedor is null)
        {
            result.AddNotification(nameof(Fornecedor), "Fornecedor não encontrado!");

            return await Task.FromResult(result);
        }

        produto.AtualizarFornecedor(fornecedor);

        var produtoEditado = await _produtoEdicaoDomainService.EditarAsync(produto, cancellationToken);

        if (produtoEditado is null)
        {
            result.AddNotification(nameof(Produto), "Produto não editado!");

            return await Task.FromResult(result);
        }

        if (!produtoEditado.IsValid)
        {
            result.AddNotifications(produtoEditado.Notifications);

            return await Task.FromResult(result);
        }

        result = _mapper.Map<ProdutoEdicaoCommandResult>(produto);

        return await Task.FromResult(result);
    }
}