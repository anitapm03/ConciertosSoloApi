using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private RepositoryPublicaciones repo;

        public PublicacionesController(RepositoryPublicaciones repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserPubli>>> GetPublicaciones()
        {
            return await this.repo.GetPublicaciones();
        }

        [HttpPost]
        public async Task<ActionResult> InsertarPublicacion
            (Publicacion publi)
        {
            await this.repo.InsertPubli(publi.IdUsuario, publi.Texto,
                publi.Imagen, publi.Fecha);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarPublicacion(int id)
        {
            await this.repo.EliminarPubli(id);
            return Ok();
        }
    }
}
