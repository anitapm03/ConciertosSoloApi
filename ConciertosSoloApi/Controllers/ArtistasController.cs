using ConciertosSoloApi.Repositories;
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

        //METODOS DE ARTISTAS
        [HttpGet]
        public async Task<ActionResult<List<Artista>>> GetArtistas()
        {
            return await
                this.repo.GetArtistasAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artista>> FindArtista(int id)
        {
            return await this.repo.FindArtista(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostArtista
            (Artista artista)
        {
            await this.repo.InsertarArtista(artista.Nombre, artista.Imagen
                , artista.Spotify, artista.Descripcion);
            return Ok();
        }

        [HttpDelete("{id}")]
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

        [HttpPut]
        //[Route("[action]/{id}/{nombre}/{loc}")]
        public async Task<ActionResult> EditarArtista(Artista artista)
        {
            await this.repo.EditarArtista(artista.IdArtista, artista.Nombre,
                artista.Spotify, artista.Descripcion);
            return Ok();
        }

    }
}
