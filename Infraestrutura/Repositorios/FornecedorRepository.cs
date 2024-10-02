using Dominio.Contratos.Repositorio;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorios;

public class FornecedorRepository : Repositorio<Fornecedor>, IFornecedorRepository
{
    public FornecedorRepository(DbContext database) : base(database)
    {
    }
}