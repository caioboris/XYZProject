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
    [Route(template: "user")]
    public class UserController: ControllerBase
    {
        [HttpGet]
        [Route(template:"listar")]
        public async Task<IActionResult> GetAsync(
            [FromServices]AppDbContext context)
        {
            var users = await context
                .users
                .AsNoTracking()
                .ToListAsync();

            return Ok(users);
        }
        [HttpPost]
        [Route(template: "criar")]
        public async Task<IActionResult> PostAsync(
                    [FromForm] AppDbContext context, [FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new User
            {
                Login = model.Login,
                Password = model.Password
            };

            try
            {
                await context.users.AddAsync(user);
                await context.SaveChangesAsync();

                return Created($"produto/criar/{user.Id}", user);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route(template: "editar/{id}")]
        public async Task<IActionResult> PutAsync(
           [FromForm] AppDbContext context, [FromBody] User model, [FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await context
            .users
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (user == null)
            {
                return NotFound();
            }
            try
            {
                user.Login = model.Login;
                user.Password = model.Password;

                context.users.Update(user);
                await context.SaveChangesAsync();

                return Ok("Usuário alterado!:\n"+user);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> DeleteAsync(
           [FromForm] AppDbContext context,  [FromRoute] string id)
        {
            var user = await context
                .users
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            try
            {
                context.users.Remove(user);
                await context.SaveChangesAsync();
                return Ok("Usuário Excluído com Sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
