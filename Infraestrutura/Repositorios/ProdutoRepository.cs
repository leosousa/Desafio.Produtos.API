using Dominio.Contratos.Repositorio;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorios;

public class ProdutoRepository : Repositorio<Produto>, IProdutoRepository
{
    public ProdutoRepository(DbContext database) : base(database)
    {
    }
}