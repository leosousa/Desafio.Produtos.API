using Flunt.Notifications;

namespace Aplicacao.CasosUso.Produto.Cadastrar;

public class ProdutoCadastroCommandResult : Notifiable<Notification>
{
    public int Id { get; set; }
}