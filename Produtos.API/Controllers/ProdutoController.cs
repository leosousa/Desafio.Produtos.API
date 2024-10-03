using Aplicacao.CasosUso.BuscaPorId;
using Aplicacao.CasosUso.Produto.Cadastrar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}