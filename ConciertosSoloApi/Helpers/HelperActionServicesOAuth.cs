using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ConciertosSoloApi.Helpers
{
    public class HelperActionServicesOAuth
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }

        public HelperActionServicesOAuth
            (IConfiguration configuration)
        {
            this.Issuer =
                configuration.GetValue<string>("ApiOAuth:Issuer");
            this.Audience =
                configuration.GetValue<string>("ApiOAuth:Audience");
            this.SecretKey =
                configuration.GetValue<string>("ApiOAuth:SecretKey");
        }

        //necesitamos un metodo para generar el token 
        //que se basa en el secret key
        public SymmetricSecurityKey GetKeyToken()
        {
            //convertimos el secret key a bytes[]
            byte[] data = Encoding.UTF8.GetBytes(this.SecretKey);

            //devolvemos la key 
            return new SymmetricSecurityKey(data);
        }

        //hemos creado esta clase para quitar codigo de program

        public Action<JwtBearerOptions> GetJwtBearerOptions()
        {
            Action<JwtBearerOptions> options =
                new Action<JwtBearerOptions>(options =>
                {
                    //indicamos que debemos validar de nuestro
                    //token: issuer, audience, time...
                    options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = this.Issuer,
                        ValidAudience = this.Audience,
                        IssuerSigningKey = this.GetKeyToken()
                    };
                });
            return options;
        }


        //metodo para indicar el esquema de la validacion
        public Action<AuthenticationOptions>
            GetAuthenticationSchema()
        {
            Action<AuthenticationOptions> options =
                new Action<AuthenticationOptions>(options =>
                {
                    options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                });
            return options;
        }
    }
}
