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
    [Route(template: "cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route(template: "listar")]
        public async Task<IActionResult> GetAsync(
            [FromServices]AppDbContext context)
        {
            var clientes = await context
                .clientes
                .AsNoTracking()
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpPost]
        [Route(template:"criar")]
        public async Task<IActionResult> PostAsync(
            [FromServices]AppDbContext context, [FromBody]Cliente model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cliente = new Cliente
            {
                Nome = model.Nome,
                DataDeNascimento = model.DataDeNascimento,
                Email = model.Email,
                Telefone = model.Telefone,
                EnderecoCliente = model.EnderecoCliente,
            };

            try
            {
                await context.clientes.AddAsync(cliente);
                await context.SaveChangesAsync();
                return Created($"cliente/criar/{cliente.Id}", cliente);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut]
        [Route(template: "editar/{id}")]
        public async Task<IActionResult> PutAsync(
           [FromForm] AppDbContext context, [FromBody] Cliente model, [FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cliente = await context
            .clientes
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (cliente == null)
            {
                return NotFound();
            }
            try
            {
                cliente.Nome= model.Nome;
                cliente.DataDeNascimento = model.DataDeNascimento;
                cliente.Email = model.Email;
                cliente.Telefone = model.Telefone;
                cliente.EnderecoCliente = model.EnderecoCliente; 

                context.clientes.Update(cliente);
                await context.SaveChangesAsync();

                return Ok("Cliente alterado!:\n" + cliente);

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
            var cliente = await context
                .clientes
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            try
            {
                context.clientes.Remove(cliente);
                await context.SaveChangesAsync();
                return Ok("Cliente Excluído com Sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
