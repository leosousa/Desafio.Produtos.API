using MediatR;

namespace Aplicacao.CasosUso.Remocao;

public record ProdutoRemocaoCommand : IRequest<ProdutoRemocaoCommandResult>
{
    public int Id { get; set; }
}