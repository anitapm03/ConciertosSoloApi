using ConciertosSoloApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Repositories
{
    public class RepositoryConciertos
    {
        private ConciertosSoloContext context;

        public RepositoryConciertos(ConciertosSoloContext context)
        {
            this.context = context;
        }

        public async Task<List<Evento>> GetEventos()
        {
            var consulta = from datos in this.context.Eventos
                           select datos;
            return consulta.ToList();
        }

        public async Task<List<Evento>> GetDestacados()
        {
            var consulta = from datos in this.context.Eventos
                           where datos.Destacado == true
                           select datos;
            return consulta.ToList();
        }

        public async Task<Evento> FindEvento(int idevento)
        {
            var consulta = from datos in this.context.Eventos
                           where datos.IdConcierto == idevento
                           select datos;
            return consulta.FirstOrDefault();
        }

        public async Task<Concierto> FindConcierto(int id)
        {
            var consulta = from datos in this.context.Conciertos
                           where datos.IdConcierto == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public async Task InsertarConcierto(string nombre, DateTime fecha, string foto,
            string entradas, int sala, string grupo)
        {
            string sql = "SP_INSERT_CONCIERTO @NOMBRE, @FECHA, " +
                "@FOTO, @ENTRADAS, @IDSALA, @GRUPO";

            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter fe = new SqlParameter("@FECHA", fecha);
            SqlParameter fo = new SqlParameter("@FOTO", foto);
            SqlParameter ent = new SqlParameter("@ENTRADAS", entradas);
            SqlParameter ids = new SqlParameter("@IDSALA", sala);
            SqlParameter gr = new SqlParameter("@GRUPO", grupo);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, nom, fe, fo, ent, ids, gr);
        }

        public async Task EditarConcierto
            (int id, string nombre, DateTime fecha,
            string entradas, int sala, string grupo)
        {
            string sql = "SP_UPDATE_CONCIERTO @ID, @NOMBRE, @FECHA, " +
                " @ENTRADAS, @IDSALA, @GRUPO";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter fe = new SqlParameter("@FECHA", fecha);
            SqlParameter ent = new SqlParameter("@ENTRADAS", entradas);
            SqlParameter ids = new SqlParameter("@IDSALA", sala);
            SqlParameter gr = new SqlParameter("@GRUPO", grupo);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, fe, ent, ids, gr);
        }

        public async Task EditarConciertoFoto
            (int id, string nombre, DateTime fecha, string foto,
            string entradas, int sala, string grupo)
        {
            string sql = "SP_UPDATE_CONCIERTO_FOTO @ID, @NOMBRE, @FECHA, " +
            " @FOTO, @ENTRADAS, @IDSALA, @GRUPO";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter fe = new SqlParameter("@FECHA", fecha);
            SqlParameter fo = new SqlParameter("@FOTO", foto);
            SqlParameter ent = new SqlParameter("@ENTRADAS", entradas);
            SqlParameter ids = new SqlParameter("@IDSALA", sala);
            SqlParameter gr = new SqlParameter("@GRUPO", grupo);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, fe, fo, ent, ids, gr);
        }

        public async Task EliminarConcierto(int id)
        {
            string sql = "SP_ELIMINARCONCIERTO @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }

        public async Task DestacarEvento(int idconcierto)
        {
            string sql = "SP_DESTACAR_EVENTO @IDCONCIERTO";
            SqlParameter pid = new SqlParameter("@IDCONCIERTO", idconcierto);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }

        public async Task NoDestacarEvento(int idconcierto)
        {
            string sql = "SP_NODESTACAR_EVENTO @IDCONCIERTO";
            SqlParameter pid = new SqlParameter("@IDCONCIERTO", idconcierto);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }
    }
}
