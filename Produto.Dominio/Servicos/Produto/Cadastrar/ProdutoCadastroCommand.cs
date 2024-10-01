using Dominio.Regras;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System.Xml.Linq;

namespace Dominio.Usecases.Produto.Cadastrar;

public class ProdutoCadastroCommand : Notifiable<Notification>, IRequest<ProdutoCadastroCommandResult>
{
    /// <summary>
    /// Descrição do produto
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Situação de atividade do produto. True = Ativo. False = Inativo
    /// </summary>
    public bool Situacao { get; set; }

    /// <summary>
    /// Data de fabricação do produto
    /// </summary>
    public DateTime DataFabricacao { get; set; }

    /// <summary>
    /// Data de validade do produto
    /// </summary>
    public DateTime DataValidade { get; set; }

    public void Validate()
    {
        AddNotifications(new ProdutoRegraNegocio()
            .DataFabricacaoNaoPodeSerMaiorQueDataValidade(DataFabricacao,DataValidade)
            .DescricaoNaoPodeSerNula(Descricao)
        );
    }
}