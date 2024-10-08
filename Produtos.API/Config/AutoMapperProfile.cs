﻿using Aplicacao.CasosUso.BuscaPorId;
using Aplicacao.CasosUso.Edicao;
using Aplicacao.CasosUso.ListaPaginada;
using Aplicacao.CasosUso.Produto.Cadastrar;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Entidades;
using Dominio.Servicos.Produto.ListaPaginada;

namespace Produtos.API.Config;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProdutoCadastroCommand, Produto>();
        CreateMap<Produto, ProdutoCadastroCommandResult>();

        CreateMap<Produto, ProdutoBuscaPorIdQueryResult>();
        CreateMap<Fornecedor, ProdutoBuscaPorIdFornecedorResult>();

        CreateMap<ProdutoListaPaginadaQuery, ProdutoListaFiltroDTO>();
        CreateMap<ListaPaginadaResultDTO<Produto>, ProdutoListaPaginadaQueryResult>();
        CreateMap<Produto, ProdutoItemResult>();

        CreateMap<ProdutoEdicaoCommand, Produto>();
        CreateMap<Produto, ProdutoEdicaoCommandResult>();
    }
}