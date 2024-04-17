using ConciertosSoloApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Repositories
{
    public class RepositoryGeneros
    {
        private ConciertosSoloContext context;

        public RepositoryGeneros(ConciertosSoloContext context)
        {
            this.context = context;
        }

        public async Task<List<Genero>> GetGeneros()
        {
            var consulta = from datos in context.Generos
                           select datos;
            return consulta.ToList();
        }

        public async Task<Genero> FindGenero(int id)
        {
            var consulta = from datos in context.Generos
                           where datos.IdGenero == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public async Task InsertGenero(string nombre)
        {
            string sql = "SP_INSERTGENERO @NOMBRE";
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);

            this.context.Database.ExecuteSqlRaw(sql, nom);
        }

        public async Task EliminarGenero(int id)
        {
            string sql = "SP_ELIMINARGENERO @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }
    }
}
