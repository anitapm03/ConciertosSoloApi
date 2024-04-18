using ConciertosSoloApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Repositories
{
    public class RepositorySesion
    {
        private ConciertosSoloContext context;

        public RepositorySesion(ConciertosSoloContext context)
        {
            this.context = context;
        }

        //COSAS DE LA SEGURIDAD
        public async Task<Usuario> LoginUsuario(string username, string password)
        {

            return await this.context.Usuarios
                .Where(x => x.Nombre == username && x.Contrasena == password)
                .FirstOrDefaultAsync();
        }


        //metodos antiguos jeje

        public async Task<List<Usuario>> GetUsuarios()
        {
            var consulta = from datos in context.Usuarios
                           select datos;
            return consulta.ToList();
        }

        public async Task<Usuario> FindUserAsync(string idusuario)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.IdUsuario == int.Parse(idusuario)
                           select datos;
            Usuario user = await consulta.FirstOrDefaultAsync();
            return user;
        }

        public async Task<Usuario> ActualizarInfoUsuario
            (int id, string nombre, string email, string bio)
        {
            string sql = "SP_UPDATE_USER @ID, @NOMBRE, @EMAIL, @BIO";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter pnom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pmail = new SqlParameter("@EMAIL", email);
            SqlParameter pbio = new SqlParameter("@BIO", bio);

            var consulta = this.context.Usuarios.FromSqlRaw(sql, pid, pnom, pmail, pbio);

            Usuario user = consulta.AsEnumerable().FirstOrDefault();

            return user;
        }

        public async Task UpdatePicture(int id, string imagen)
        {
            string sql = "SP_UPDATE_USER_FOTO @ID, @IMAGEN";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter pfoto = new SqlParameter("@IMAGEN", imagen);

            var consulta = this.context.Database.ExecuteSqlRaw(sql, pid, pfoto);
        }

        public async Task EliminarUsuario(int id)
        {
            string sql = "SP_ELIMINARUSER @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }
    }
}
/*public string InsertarUsuario
            (string nombre, string email,
            string contrasena, string bio)
        {

            string sql = "SP_INSERTAR_USUARIO @ID, @NOMBRE, @EMAIL, " +
                "@CONTRASENA, @BIO, @IMAGEN, @SALT, @ACTIVO, @TOKENMAIL";

            SqlParameter pid = new SqlParameter("@ID", 1);
            SqlParameter pnom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pmail = new SqlParameter("@EMAIL", email);

            string salt = HelperCryptography.GenerateSalt();
            byte[] passw = HelperCryptography.EncryptPassword(contrasena, salt);

            SqlParameter pcont = new SqlParameter("@CONTRASENA", passw);
            SqlParameter pbio = new SqlParameter("@BIO", bio);
            SqlParameter pimg = new SqlParameter("@IMAGEN", "defaultprofile.jpg");

            SqlParameter psalt = new SqlParameter("@SALT", salt);

            bool activo = false;
            string token = HelperTools.GenerateTokenMail();

            SqlParameter pact = new SqlParameter("@ACTIVO", activo);
            SqlParameter ptoken = new SqlParameter("@TOKENMAIL", token);

            this.context.Database.ExecuteSqlRaw(sql, pid, pnom, pmail,
                pcont, pbio, pimg, psalt, pact, ptoken);

            return token;
        }

        public void UpdatePassw(int id, string contrasena)
        {
            string salt = HelperCryptography.GenerateSalt();
            byte[] passw = HelperCryptography.EncryptPassword(contrasena, salt);

            string sql = "SP_UPDATE_USER_PASSW @ID, @PASW, @SALT";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter ppas = new SqlParameter("@PASW", passw);
            SqlParameter psalt = new SqlParameter("@SALT", salt);

            var consulta = this.context.Database.ExecuteSqlRaw(sql, pid, ppas, psalt);

        }
 
 */



/*public async Task<Usuario> LogInUserAsync(string email, string contrasena)
{
    string sql = "SP_FIND_EMAIL @EMAIL";

    SqlParameter pmail = new SqlParameter("@EMAIL", email);

    var consulta = this.context.Usuarios.FromSqlRaw(sql, pmail);

    Usuario user = consulta.AsEnumerable().FirstOrDefault();

    if (user == null)
    {
        return null;
    }
    else
    {
        string salt = user.Salt;
        byte[] temp =
            HelperCryptography.EncryptPassword(contrasena, salt);
        byte[] passUser = user.Contrasena;
        bool response =
            HelperCryptography.CompareArrays(temp, passUser);
        if (response == true)
        {
            return user;
        }
        else
        {
            return null;
        }
    }
}*/

/*public async Task ActivateUserAsync(string token)
{
    //BUSCAMOS EL USUARIO POR SU TOKEN
    //Usuario user = await this.FindUserTokenAsync(token);
    bool activo = true;

    SqlParameter ptoken = new SqlParameter("@TOKEN", token);
    SqlParameter pact = new SqlParameter("@ACTIVO", activo);
    string sql = "SP_FINDTOKEN_ACTIVATEUSER @TOKEN, @ACTIVO";


    this.context.Database.ExecuteSqlRaw(sql, ptoken, pact);
}

public async Task<Usuario> FindUserTokenAsync(string token)
{
    var consulta = from datos in this.context.Usuarios
                   where datos.TokenMail == token
                   select datos;
    Usuario user = await consulta.FirstOrDefaultAsync();
    return user;
}*/