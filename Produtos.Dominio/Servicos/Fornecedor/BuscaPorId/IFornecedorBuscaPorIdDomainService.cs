namespace Dominio.Servicos.Fornecedor.BuscaPorId;

public interface IFornecedorBuscaPorIdDomainService
{
    Task<Entidades.Fornecedor?> BuscaPorId(int id);
}
