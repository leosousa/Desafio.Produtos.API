
using Dominio.Contratos.Repositorio;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestrutura.Repositorios;

public abstract class Repositorio<T> : IRepositorio<T> where T : Entidade
{
    private readonly DbContext _database;
    private readonly DbSet<T> _dbSet;

    public Repositorio(DbContext database)
    {
        _database = database;
        _dbSet = _database.Set<T>();
    }

    public async Task<T?> BuscarPorIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<T> CadastrarAsync(T produto)
    {
        _dbSet.Add(produto);

        await _database.SaveChangesAsync();

        return produto;
    }

    public async Task<T> EditarAsync(T produto)
    {
        _database.Update(produto);

        await _database.SaveChangesAsync();

        return produto;
    }

    public async Task<IEnumerable<T>> ListarAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> ListarAsync(Expression<Func<T, bool>> predicado, int numeroPagina, int tamanhoPagina)
    {
        return await _dbSet
            .Where(predicado)
            .Skip(numeroPagina * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();
    }

    public async Task<bool> RemoverAsync(T produto)
    {
        _dbSet.Remove(produto);

        var affectedRows = await _database.SaveChangesAsync();

        return (affectedRows >= 1);
    }
}
