using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestrutura.BancoDados;

public static class DatabaseMigrationConfig
{
    public static void UseUpdateDatabase(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            serviceScope.ServiceProvider.GetService<ProdutoDbContext>()?.Database.Migrate();
        }
    }
}