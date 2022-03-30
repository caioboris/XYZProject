using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYZProject.Data;
using XYZProject.Models;

namespace XYZProject.Controllers
{
    [ApiController]
    [Route(template: "produto")]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route(template: "listar")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var produtos = await context
                .produtos
                .AsNoTracking()
                .ToListAsync();
            return Ok(produtos);
        }

        [HttpPost]
        [Route(template: "criar")]
        public async Task<IActionResult> PostAsync(
            [FromForm] AppDbContext context, [FromBody] Produto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var produto = new Produto
            {
                Descricao = model.Descricao,
                Photo = model.Photo,
                PrecoUnitario = model.PrecoUnitario
            };

            try
            {
                await context.produtos.AddAsync(produto);
                await context.SaveChangesAsync();

                return Created($"produto/criar/{produto.Id}", produto);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route(template: "editar/{id}")]
        public async Task<IActionResult> PutAsync(
           [FromForm] AppDbContext context, [FromBody] Produto model, [FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var produto = await context
            .produtos
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (produto == null)
            {
                return NotFound();
            }
            try
            {
                produto.Descricao = model.Descricao;
                produto.Photo = model.Photo;
                produto.PrecoUnitario = model.PrecoUnitario;


                context.produtos.Update(produto);
                await context.SaveChangesAsync();

                return Ok(produto);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> DeleteAsync(
           [FromForm] AppDbContext context, [FromRoute] string id)
        {
            var produto = await context
                .produtos
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            try
            {
                context.produtos.Remove(produto);
                await context.SaveChangesAsync();
                return Ok("Produto Excluído com Sucesso!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
