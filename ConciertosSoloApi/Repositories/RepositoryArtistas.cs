using ConciertosSoloApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Repositories
{
    public class RepositoryArtistas
    {
        private ConciertosSoloContext context;

        public RepositoryArtistas( ConciertosSoloContext context)
        {
            this.context = context;
        }

        public async Task<List<Artista>> GetArtistasAsync()
        {
            return await this.context.Artistas.ToListAsync();
        }

        public async Task<Artista> FindArtista(int id)
        {
            var consulta = from datos in this.context.Artistas
                           where datos.IdArtista == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public async Task InsertarArtista
            (string nombre, string imagen,
            string spotify, string descripcion)
        {
            string sql = "SP_INSERT_ARTISTA @NOMBRE, @IMAGEN," +
                 "@SPOTIFY, @DESCRIPCION";
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter img = new SqlParameter("@IMAGEN", imagen);
            SqlParameter spt = new SqlParameter("@SPOTIFY", spotify);
            SqlParameter desc = new SqlParameter("@DESCRIPCION", descripcion);

            this.context.Database.ExecuteSqlRaw(sql, nom, img, spt, desc);
        }

        public async Task EliminarArtista(int id)
        {
            string sql = "SP_ELIMINARARTISTA @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }

        public async Task EditarArtista
            (int id, string nombre,
             string spotify, string descripcion)
        {
            string sql = "SP_UPDATE_ARTISTA @ID, @NOMBRE, @SPOTIFY, " +
                " @DESCRIPCION";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter sp = new SqlParameter("@SPOTIFY", spotify);
            SqlParameter desc = new SqlParameter("@DESCRIPCION", descripcion);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, sp, desc);
        }

        public async Task EditarArtistaFoto
            (int id, string nombre, string imagen,
             string spotify, string descripcion)
        {
            string sql = "SP_UPDATE_ARTISTA_FOTO @ID, @NOMBRE, @IMAGEN," +
                " @SPOTIFY, @DESCRIPCION";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter img = new SqlParameter("@IMAGEN", imagen);
            SqlParameter sp = new SqlParameter("@SPOTIFY", spotify);
            SqlParameter desc = new SqlParameter("@DESCRIPCION", descripcion);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, img, sp, desc);
        }
    }
}
