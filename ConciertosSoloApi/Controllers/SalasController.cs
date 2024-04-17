using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private RepositorySalas repo;

        public SalasController(RepositorySalas repo)
        {
            this.repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<Sala>>> GetSalas()
        {
            return await
                this.repo.GetSalas();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> FindSala(int id)
        {
            return await this.repo.FindSala(id);
        }

        [HttpPost]
        public async Task<ActionResult> InsertarSala
            (Sala sala)
        {
            await this.repo.CrearSala(sala.Direccion, sala.Nombre, sala.IdProvincia);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarSala(int id)
        {
            await this.repo.EliminarSala(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditarSala(Sala sala)
        {
            await this.repo.EditarSala(sala.IdSala, sala.Nombre,
                sala.Direccion, sala.IdProvincia);
            return Ok();
        }

    }
}
