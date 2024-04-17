using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionesController : ControllerBase
    {
        private RepositoryRelaciones repo;

        public RelacionesController(RepositoryRelaciones repo)
        {
            this.repo = repo;
        }

        [HttpGet("{idartista}")]
        public async Task<ActionResult<List<ArtistaConcierto>>>
            GetConciertosArtista(int idartista)
        {
            return await
                this.repo.GetConciertosArtista(idartista);
        }

        [HttpGet("{idconcierto}")]
        public async Task<ActionResult<List<ArtistaConcierto>>>
            GetArtistasConcierto(int idconcierto)
        {
            return await this.repo.GetArtistasConcierto(idconcierto);
        }

        [HttpGet("{idartista}")]
        public async Task<ActionResult<List<ArtistaGenero>>>
            GetGenerosArtista(int idartista)
        {
            return await this.repo.GetGenerosArtista(idartista);
        }

        [HttpPost("{idartista}/{idconcierto}")]
        public async Task<ActionResult> InsertarArtistaConcierto
            (int idartista, int idconcierto)
        {
            await this.repo.InsertarArtistaConcierto(idartista, idconcierto);
            return Ok();
        }

        [HttpPost("{idartista}/{idgenero}")]
        public async Task<ActionResult> InsertarArtistaGenero
            (int idartista, int idgenero)
        {
            await this.repo.InsertarArtistaConcierto(idartista, idgenero);
            return Ok();
        }

        [HttpDelete("{idconcierto}/{idartista}")]
        public async Task<ActionResult> EliminarRelacionConcierto
            (int idconcierto, int idartista)
        {
            await this.repo.EliminarRelacionConcierto(idconcierto, idartista);
            return Ok();
        }

        [HttpDelete("{idartista}/{idgenero}")]
        public async Task<ActionResult> EliminarRelacionGenero
            (int idartista, int idgenero)
        {
            await this.repo.EliminarRelacionGenero(idartista, idgenero);
            return Ok();
        }
    }
}
