using Dominio.Contratos.Repositorio;
using Dominio.Entidades;
using Infraestrutura.BancoDados;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorios;

public class FornecedorRepository : Repositorio<Fornecedor>, IFornecedorRepository
{
    public FornecedorRepository(ProdutoDbContext database) : base(database)
    {
    }
}