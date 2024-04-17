using ConciertosSoloApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Repositories
{
    public class RepositoryPublicaciones
    {
        private ConciertosSoloContext context;

        public RepositoryPublicaciones(ConciertosSoloContext context)
        {
            this.context = context;
        }

        public async Task<List<UserPubli>> GetPublicaciones()
        {
            var consulta = from datos in context.PublicacionesUsuarios
                           orderby datos.Fecha descending
                           select datos;

            return consulta.ToList();
        }

        public async Task InsertPubli(int iduser, string texto,
            string imagen, DateTime fecha)
        {
            string sql = "SP_INSERT_PUBLI @USER, @TEXTO, @IMAGEN, @FECHA";

            SqlParameter user = new SqlParameter("@USER", iduser);
            SqlParameter txt = new SqlParameter("@TEXTO", texto);
            SqlParameter img = new SqlParameter("@IMAGEN", imagen);
            SqlParameter fe = new SqlParameter("@FECHA", fecha);

            this.context.Database.ExecuteSqlRaw(sql, user, txt, img, fe);
        }

        public async Task EliminarPubli(int id)
        {
            string sql = "SP_ELIMINARPUBLI @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);

        }
    }
}
