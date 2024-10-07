using Aplicacao.CasosUso.Edicao;
using Aplicacao.CasosUso.Remocao;
using Bogus;
using Flunt.Notifications;

namespace TesteUnitario.Aplicacao.Mocks;

public class ProdutoRemocaoCommandResultMock : Faker<ProdutoRemocaoCommandResult>
{
    private ProdutoRemocaoCommandResultMock() : base("pt_BR")
    {
        RuleFor(produto => produto.IsValid, faker => faker.Random.Bool());

        RuleFor(produto => produto.Notifications, NotificationMock.GerarObjetoLista());
    }

    public static ProdutoRemocaoCommandResult GerarObjeto()
    {
        return new ProdutoRemocaoCommandResultMock().Generate();
    }

    public static ProdutoRemocaoCommandResult GerarObjetoComNotificacao(Notification notificacao)
    {
        var result = new ProdutoRemocaoCommandResultMock().Generate();

        result.AddNotification(notificacao);

        return result;
    }
}