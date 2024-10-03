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
}