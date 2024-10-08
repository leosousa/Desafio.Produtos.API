using Dominio.Contratos.Repositorio;
using Flunt.Notifications;

namespace Dominio.Servicos.Fornecedor.BuscaPorId;

public class FornecedorBuscaPorIdDomainService : Notifiable<Notification>, IFornecedorBuscaPorIdDomainService
{
    private readonly IFornecedorRepository _fornecedorRepository;

    public FornecedorBuscaPorIdDomainService(IFornecedorRepository fornecedorRepository)
    {
        _fornecedorRepository = fornecedorRepository;
    }

    public async Task<Entidades.Fornecedor?> BuscaPorId(int id)
    {
        var fornecedor = await _fornecedorRepository.BuscarPorIdAsync(id);

        return await Task.FromResult(fornecedor);
    }
}