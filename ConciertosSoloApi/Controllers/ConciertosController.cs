using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConciertosController : ControllerBase
    {
        private RepositoryConciertos repo;

        public ConciertosController(RepositoryConciertos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> GetEventos()
        {
            return await this.repo.GetEventos();
        }

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> GetEventosDestacados()
        {
            return await this.repo.GetDestacados();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> FindEvento(int id)
        {
            return await this.repo.FindEvento(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Concierto>> FindConcierto(int id)
        {
            return await this.repo.FindConcierto(id);
        }

        [HttpPost]
        public async Task<ActionResult> InsertarConcierto(Concierto concierto)
        {
            await this.repo.InsertarConcierto(concierto.Nombre, concierto.Fecha,
                concierto.Imagen, concierto.Entradas, concierto.IdSala, concierto.Grupo);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditarConcierto(Concierto concierto)
        {
            await this.repo.EditarConcierto(concierto.IdConcierto, concierto.Nombre, 
                concierto.Fecha, concierto.Entradas, concierto.IdSala, concierto.Grupo);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditarConciertoFoto(Concierto concierto)
        {
            await this.repo.EditarConciertoFoto(concierto.IdConcierto, concierto.Nombre,
                concierto.Fecha, concierto.Imagen, concierto.Entradas, concierto.IdSala, concierto.Grupo);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> DestacarConcierto(int id)
        {
            await this.repo.DestacarEvento(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> NoDestacarConcierto(int id)
        {
            await this.repo.NoDestacarEvento(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarConcierto(int id)
        {
            await this.repo.EliminarConcierto(id);
            return Ok();
        }
    }
}
