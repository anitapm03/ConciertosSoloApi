using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeticionesController : ControllerBase
    {
        private RepositoryPeticiones repo;

        public PeticionesController(RepositoryPeticiones repo)
        {
            this.repo = repo;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<PeticionEvento>>> GetPeticiones()
        {
            return await this.repo.GetPeticiones();
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Peticion>>> GetListaPeticiones()
        {
            return await this.repo.GetListaPeticiones();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertarPeticion
            (PeticionEvento peticion)
        {
            await this.repo.InsertarPeticion(peticion.NombreArtista, peticion.IdProvincia,
                peticion.Fecha, peticion.Telefono);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> EliminarPeticion(int id)
        {
            await this.repo.EliminarPeticion(id);
            return Ok();
        }

    }
}
