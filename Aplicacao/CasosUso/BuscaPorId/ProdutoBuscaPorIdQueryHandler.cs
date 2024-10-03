using Aplicacao.CasosUso.Produto.Cadastrar;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Servicos.Produto.BuscaPorId;
using MediatR;

namespace Aplicacao.CasosUso.BuscaPorId;

public class ProdutoBuscaPorIdQueryHandler :
     IRequestHandler<ProdutoBuscaPorIdQuery, ProdutoBuscaPorIdQueryResult?>
{
    private readonly IProdutoBuscaPorIdDomainService _produtoBuscaPorIdDomainService;
    private readonly IMapper _mapper;

    public ProdutoBuscaPorIdQueryHandler(IProdutoBuscaPorIdDomainService produtoBuscaPorIdDomainService, IMapper mapper)
    {
        _produtoBuscaPorIdDomainService = produtoBuscaPorIdDomainService;
        _mapper = mapper;
    }

    public async Task<ProdutoBuscaPorIdQueryResult?> Handle(ProdutoBuscaPorIdQuery request, CancellationToken cancellationToken)
    {
        ProdutoBuscaPorIdQueryResult result = new();

        var produto = await _produtoBuscaPorIdDomainService.BuscarPorIdAsync(request.Id, cancellationToken);

        if (!_produtoBuscaPorIdDomainService.IsValid)
        {
            result.AddNotifications(_produtoBuscaPorIdDomainService.Notifications);

            return await Task.FromResult(result);
        }

        result = _mapper.Map<ProdutoBuscaPorIdQueryResult>(produto);

        return await Task.FromResult(result);
    }
}