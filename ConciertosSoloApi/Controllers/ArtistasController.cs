using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistasController : ControllerBase
    {
        private RepositoryArtistas repo;

        public ArtistasController(RepositoryArtistas repo)
        {
            this.repo = repo;
            
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Artista>>> GetArtistas()
        {
            return await
                this.repo.GetArtistasAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Artista>> FindArtista(int id)
        {
            return await this.repo.FindArtista(id);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertarArtista
            (Artista artista)
        {
            await this.repo.InsertarArtista(artista.Nombre, artista.Imagen
                , artista.Spotify, artista.Descripcion);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> EliminarArtista(int id)
        {
            if (await this.repo.FindArtista(id) == null)
            {
                return NotFound();
            }
            else
            {
                await this.repo.EliminarArtista(id);
                return Ok();
            }
        }

        [Authorize]
        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> EditarArtista(Artista artista)
        {
            await this.repo.EditarArtista(artista.IdArtista, artista.Nombre,
                artista.Spotify, artista.Descripcion);
            return Ok();
        }

    }
}
