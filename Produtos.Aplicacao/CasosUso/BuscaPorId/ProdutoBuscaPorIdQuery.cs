using MediatR;

namespace Aplicacao.CasosUso.BuscaPorId;

public record ProdutoBuscaPorIdQuery : IRequest<ProdutoBuscaPorIdQueryResult>
{
    public int Id { get; set; }
}