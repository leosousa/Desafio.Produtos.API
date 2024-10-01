using AutoMapper;
using Dominio.Contratos.Repositorio;
using Flunt.Notifications;
using MediatR;

namespace Dominio.Usecases.Produto.Cadastrar;

public class ProdutoCadastroCommandHandlerService : Notifiable<Notification>, IRequestHandler<ProdutoCadastroCommand, ProdutoCadastroCommandResult?>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public ProdutoCadastroCommandHandlerService(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<ProdutoCadastroCommandResult?> Handle(ProdutoCadastroCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        if (!request.IsValid)
        {
            AddNotifications(request);
        }

        var produto = _mapper.Map<Entidades.Produto>(request);

        var produtoCadastrado = await _produtoRepository.CadastrarAsync(produto);

        return await Task.FromResult(new ProdutoCadastroCommandResult(produtoCadastrado));
    }
}