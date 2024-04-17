using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private RepositoryArtistas repositoryArtistas;
        private RepositoryConciertos repositoryConciertos;
        private RepositoryGeneros repositoryGeneros;
        private RepositoryPeticiones repositoryPeticiones;
        private RepositoryProvincias repositoryProvincias;
        private RepositoryPublicaciones repositoryPublicaciones;
        private RepositoryRelaciones repositoryRelaciones;
        private RepositorySalas repositorySalas;
        private RepositorySesion repositorySesion;

        public ApiController(RepositoryArtistas repositoryArtistas, 
            RepositoryConciertos repositoryConciertos, 
            RepositoryGeneros repositoryGeneros, 
            RepositoryPeticiones repositoryPeticiones, 
            RepositoryProvincias repositoryProvincias, 
            RepositoryPublicaciones repositoryPublicaciones, 
            RepositoryRelaciones repositoryRelaciones, 
            RepositorySalas repositorySalas, 
            RepositorySesion repositorySesion)
        {
            this.repositoryArtistas = repositoryArtistas;
            this.repositoryConciertos = repositoryConciertos;
            this.repositoryGeneros = repositoryGeneros;
            this.repositoryPeticiones = repositoryPeticiones;
            this.repositoryProvincias = repositoryProvincias;
            this.repositoryPublicaciones = repositoryPublicaciones;
            this.repositoryRelaciones = repositoryRelaciones;
            this.repositorySalas = repositorySalas;
            this.repositorySesion = repositorySesion;
        }

        //METODOS DE ARTISTAS
        [HttpGet]
        public async Task<ActionResult<List<Artista>>> GetArtistas()
        {
            return await
                this.repositoryArtistas.GetArtistasAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artista>> GetArtistas(int id)
        {
            return await this.repositoryArtistas.FindArtista(id);
        }


    }
}
