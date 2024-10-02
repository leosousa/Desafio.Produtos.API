using Flunt.Notifications;
using Flunt.Validations;

namespace Dominio.Regras;

public class ProdutoRegraNegocio : Contract<Notification>
{
    public ProdutoRegraNegocio DescricaoNaoPodeSerNula(string descricao)
    {
        if (string.IsNullOrEmpty(descricao))
        {
            AddNotification(nameof(Entidades.Produto.Descricao), "Descrição é campo obrigatório");
        }

        return this;
    }

    public ProdutoRegraNegocio DataFabricacaoNaoPodeSerMaiorQueDataValidade(
        DateTime dataFabricacao,
        DateTime dataValidade)
    {
        if (dataFabricacao >= dataValidade)
        {
            AddNotification(nameof(Entidades.Produto.DataFabricacao), "Data de fabricação não pode ser maior ou igual do que a validade");
        }

        return this;
    }
}