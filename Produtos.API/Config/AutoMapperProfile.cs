using Aplicacao.CasosUso.Produto.Cadastrar;
using AutoMapper;
using Dominio.Entidades;

namespace Produtos.API.Config;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProdutoCadastroCommand, Produto>();
        CreateMap<Produto, ProdutoCadastroCommandResult>();
    }
}