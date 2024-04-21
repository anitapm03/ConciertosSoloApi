using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await
                this.repo.GetUsuarios();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Usuario>> FindUsuario(string id)
        {
            return await this.repo.FindUserAsync(id);
        }

        [HttpGet]
        [Route("[action]/{username}")]
        public async Task<ActionResult<Usuario>> FindUsuarioNombre(string username)
        {
            return await this.repo.FindUserNombre(username);
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            await this.repo.EliminarUsuario(id);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> EditarUsuario(Usuario usuario)
        {
            await this.repo.ActualizarInfoUsuario(usuario.IdUsuario, usuario.Nombre,
                usuario.Email, usuario.Bio);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("[action]/{id}/{imagen}")]
        public async Task<ActionResult> EditarFotoUsuario(int id, string imagen)
        {
            await this.repo.UpdatePicture(id, imagen);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RegistrarUsuario
            (Usuario user)
        ///{username}/{email}/{passw}/{bio}
        //string username, string email, string passw, string bio
        //username, email, passw, bio
        {
            await this.repo.InsertarUsuario(user.Nombre, user.Email, user.Contrasena, user.Bio);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("[action]/{id}/{contrasena}")]
        public async Task<ActionResult> UpdatePassw(int id, string contrasena)
        {
            await this.repo.UpdatePassw(id, contrasena);
            return Ok();
        }

    }
}
/*[HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertarSala
            (Sala sala)
        {
            await this.repo.CrearSala(sala.Direccion, sala.Nombre, sala.IdProvincia);
            return Ok();
        }*/