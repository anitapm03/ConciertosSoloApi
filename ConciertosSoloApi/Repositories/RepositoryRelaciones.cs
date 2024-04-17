using ConciertosSoloApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Repositories
{
    public class RepositoryRelaciones
    {
        private ConciertosSoloContext context;

        public RepositoryRelaciones(ConciertosSoloContext context)
        {
            this.context = context;
        }

        public async Task<List<ArtistaConcierto>> GetConciertosArtista(int idartista)
        {
            var consulta = from datos in context.RelacionesConcierto
                           where datos.IdArtista == idartista
                           select datos;
            return consulta.ToList();
        }

        public async Task<List<ArtistaConcierto>> GetArtistasConcierto(int idconcierto)
        {
            var consulta = from datos in context.RelacionesConcierto
                           where datos.IdConcierto == idconcierto
                           select datos;
            return consulta.ToList();
        }

        public async Task InsertarArtistaConcierto(int idartista, int idconcierto)
        {
            string sql = "ADD_ARTISTACONCIERTO @IDCONCIERTO, @IDARTISTA";
            SqlParameter pcon = new SqlParameter("@IDCONCIERTO", idconcierto);
            SqlParameter part = new SqlParameter("@IDARTISTA", idartista);

            this.context.Database.ExecuteSqlRaw
                (sql, pcon, part);

        }

        public async Task InsertarArtistaGenero(int idartista, int idgenero)
        {
            string sql = "ADD_GENEROARTISTA @IDARTISTA, @IDGENERO";

            SqlParameter part = new SqlParameter("@IDARTISTA", idartista);
            SqlParameter pgen = new SqlParameter("@IDGENERO", idgenero);

            this.context.Database.ExecuteSqlRaw
                (sql, part, pgen);
        }

        public async Task<List<ArtistaGenero>> GetGenerosArtista(int idartista)
        {
            var consulta = from datos in context.RelacionesGenero
                           where datos.IdArtista == idartista
                           select datos;
            return consulta.ToList();
        }

        public async Task EliminarRelacionConcierto(int idconcierto, int idartista)
        {
            string sql = "DEL_ARTISTACONCIERTO @IDCONCIERTO, @IDARTISTA";

            SqlParameter pcon = new SqlParameter("@IDCONCIERTO", idconcierto);
            SqlParameter part = new SqlParameter("@IDARTISTA", idartista);

            this.context.Database.ExecuteSqlRaw
                (sql, pcon, part);
        }

        public async Task EliminarRelacionGenero(int idartista, int idgenero)
        {
            string sql = "DEL_GENEROARTISTA @IDARTISTA, @IDGENERO";

            SqlParameter part = new SqlParameter("@IDARTISTA", idartista);
            SqlParameter pgen = new SqlParameter("@IDGENERO", idgenero);

            this.context.Database.ExecuteSqlRaw
                (sql, part, pgen);
        }
    }
}
