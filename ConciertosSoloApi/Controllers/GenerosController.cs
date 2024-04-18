using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private RepositoryGeneros repo;

        public GenerosController(RepositoryGeneros repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Genero>>> GetGeneros()
        {
            return await this.repo.GetGeneros();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Genero>> FindGenero(int id) 
        { 
            return await this.repo.FindGenero(id);
        }

        [Authorize]
        [HttpPut]
        [Route("[action]/{nombre}")]
        public async Task<ActionResult> InsertarGenero(string nombre)
        {
            await this.repo.InsertGenero(nombre);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> DeleteGenero(int id)
        {
            await this.repo.EliminarGenero(id);
            return Ok();
        }
        
    }
}
