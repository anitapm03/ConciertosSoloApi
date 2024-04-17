using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        private RepositorySesion repo;

        public SesionController(RepositorySesion repo)
        {
            this.repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await
                this.repo.GetUsuarios();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> FindUsuario(string id)
        {
            return await this.repo.FindUserAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            await this.repo.EliminarUsuario(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditarUsuario(Usuario usuario)
        {
            await this.repo.ActualizarInfoUsuario(usuario.IdUsuario, usuario.Nombre,
                usuario.Email, usuario.Bio);
            return Ok();
        }

        [HttpPut("{id}/{imagen}")]
        public async Task<ActionResult> EditarFotoUsuario(int id, string imagen)
        {
            await this.repo.UpdatePicture(id, imagen);
            return Ok();
        }
    }
}
