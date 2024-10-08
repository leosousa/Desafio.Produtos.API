using AutoMapper;
using Dominio.DTOs;
using Dominio.Servicos.Produto.ListaPaginada;
using Flunt.Notifications;
using MediatR;
using ProdutoEntidade = Dominio.Entidades.Produto;

namespace Aplicacao.CasosUso.ListaPaginada;

public class ProdutoListaPaginadaQueryHandler : Notifiable<Notification>,
    IRequestHandler<ProdutoListaPaginadaQuery, ProdutoListaPaginadaQueryResult?>
{
    private readonly IProdutoListaDomainService _produtoListaService;
    private readonly IMapper _mapper;

    public ProdutoListaPaginadaQueryHandler(IProdutoListaDomainService produtoListaService, IMapper mapper)
    {
        _produtoListaService = produtoListaService;
        _mapper = mapper;
    }

    public async Task<ProdutoListaPaginadaQueryResult?> Handle(ProdutoListaPaginadaQuery request, CancellationToken cancellationToken)
    {
        var filtros = _mapper.Map<ProdutoListaFiltroDTO>(request);

        ListaPaginadaResultDTO<ProdutoEntidade> produtos = null;

        if (filtros is not null)
        {
            produtos = await _produtoListaService.ListarAsync(filtros, request.NumeroPagina, request.TamanhoPagina);
        }
        else
        {
            produtos = await _produtoListaService.ListarAsync(filtros!);
        }
        

        var result = _mapper.Map<ProdutoListaPaginadaQueryResult>(produtos);

        return await Task.FromResult(result);
    }
}