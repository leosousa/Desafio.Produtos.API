using Aplicacao.CasosUso.BuscaPorId;
using Aplicacao.CasosUso.Edicao;
using Aplicacao.CasosUso.ListaPaginada;
using Aplicacao.CasosUso.Produto.Cadastrar;
using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Produtos.API.Controllers;

[Route("api/produtos")]
public class ProdutoController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public ProdutoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cadastra um novo produto
    /// </summary>
    /// <param name="produto">Produto a ser cadastrado</param>
    /// <returns>Id do novo produto cadastrado</returns>
    [HttpPost]
    public async Task<IActionResult> Create(ProdutoCadastroCommand produto)
    {
        var registered = await _mediator.Send(produto);

        if (registered is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (registered.Notifications.Count > 0)
        {
            return BadRequest(registered.Notifications);
        }

        return CreatedAtAction("GetById",
           new
           {
               id = registered.Id
           },
           registered
        );
    }

    /// <summary>
    /// Busca um produto já cadastrado pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador do produto</param>
    /// <returns>Produto encontrado</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new ProdutoBuscaPorIdQuery { Id = id });

        if (result is null) return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Busca paginada de produtos com filtros informados
    /// </summary>
    /// <param name="descricao">Descrição do produto</param>
    /// <param name="situacao">Situação do produto: Ativo = true; Inativo = false</param>
    /// <param name="dataFabricacao">Data de fabricação do produto</param>
    /// <param name="dataValidade">Data de validade do produto</param>
    /// <param name="idFornecedor">Identificador do fornecedor do produto</param>
    /// <param name="numeroPagina">Número da página</param>
    /// <param name="tamanhoPagina">Tamanho da página</param>
    /// <returns>Lista paginada de produtos encontrados</returns>
    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery]string? descricao, 
        [FromQuery] bool? situacao, 
        [FromQuery] DateTime? dataFabricacao,
        [FromQuery] DateTime? dataValidade,
        [FromQuery] int? idFornecedor,
        [FromQuery] int numeroPagina = 1,
        [FromQuery] int tamanhoPagina = 10)
    {
        var filtros = new ProdutoListaPaginadaQuery
        {
            Descricao = descricao,
            Situacao = situacao,
            DataFabricacao = dataFabricacao,
            DataValidade = dataValidade,
            IdFornecedor = idFornecedor,
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina
        };

        var result = await _mediator.Send(filtros);

        if (result is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (result.Itens.IsNullOrEmpty())
        {
            return NotFound(result);
        }
            
        return Ok(result);
    }

    /// <summary>
    /// Edita um produto já cadastrado
    /// </summary>
    /// <param name="id" > Id do produto a ser alterado</param>
    /// <param name="produto">Produto a ser alterado</param>
    /// <returns>Produto editado</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] ProdutoEdicaoCommand produto)
    {
        if (produto is not null) produto.Id = id;

        var produtoEditado = await _mediator.Send(produto!);

        if (produtoEditado is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (produtoEditado.Notifications.Count > 0)
        {
            return BadRequest(produtoEditado.Notifications);
        }

        return Ok(produtoEditado);
    }
}