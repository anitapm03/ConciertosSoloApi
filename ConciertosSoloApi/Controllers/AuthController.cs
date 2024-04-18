using ConciertosSoloApi.Helpers;
using ConciertosSoloApi.Models;
using ConciertosSoloApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProyectoWebCSNetCore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConciertosSoloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositorySesion repo;
        //cuando generemos el token, debemos integrar
        //dentro de dicho token, issuer, audience...
        //para que lo valide cuando nos lo envien
        private HelperActionServicesOAuth helper;

        public AuthController(RepositorySesion repo,
            HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        //necesitamos un metodo post para validar el
        //user y que recibira LoginModel
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            //buscamos al empleado en nuestro repo
            Usuario user =
                await this.repo.LoginUsuario
                (model.UserName, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                //debemos crear unas credenciales para incluuirlas
                //dentro del token y que estaran compuestas por el 
                //secret key cifrado y el tipo de cifrado que 
                //deseemos incluir en eñ token
                SigningCredentials credentials =
                    new SigningCredentials(
                        this.helper.GetKeyToken(),
                        SecurityAlgorithms.HmacSha256);

                //convertimos el emp a json
                string jsonUsuario =
                    JsonConvert.SerializeObject(user);
                //creamos un array de claims con toda la info que 
                //queramos guardar en el token
                Claim[] info = new[]
                {
                    new Claim("UserData", jsonUsuario)
                };

                //el token se genera con una clase y debemos indicar
                //los elementos que almacenará dentro de dicho token
                JwtSecurityToken token = new JwtSecurityToken(
                    claims: info,
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    notBefore: DateTime.UtcNow
                    );
                //por ultimo devolvemos una respuesta afirmativa 
                //con un objeto anonimo en formato JSON
                return Ok(
                    new
                    {
                        response =
                        new JwtSecurityTokenHandler()
                        .WriteToken(token)
                    });
            }
        }
    }
}

