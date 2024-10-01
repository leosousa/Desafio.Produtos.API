namespace Dominio.Servicos.Fornecedor.BuscaPorId;

public interface IFornecedorBuscaPorIdService
{
    Task<Entidades.Fornecedor> BuscaPorId(int id);
}
