using Dominio.Contratos.Repositorio;
using Dominio.Entidades;
using Infraestrutura.BancoDados;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorios;

public class ProdutoRepository : Repositorio<Produto>, IProdutoRepository
{
    public ProdutoRepository(ProdutoDbContext database) : base(database)
    {
    }

    public override async Task<Produto?> BuscarPorIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Fornecedor)
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }
}