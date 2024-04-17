using ConciertosSoloApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Repositories
{
    public class RepositorySalas
    {
        private ConciertosSoloContext context;

        public RepositorySalas(ConciertosSoloContext context)
        {
            this.context = context;
        }

        public async Task<List<Sala>> GetSalas()
        {
            var consulta = from datos in this.context.Salas
                           select datos;
            return consulta.ToList();
        }

        public async Task<Sala> FindSala(int id)
        {
            var consulta = from datos in this.context.Salas
                           where datos.IdSala == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public async Task CrearSala(string direccion, string nombre, int prov)
        {
            string sql = "SP_INSERT_SALA @DIRECCION, @NOMBRE, @PROV";

            SqlParameter dir = new SqlParameter("@DIRECCION", direccion);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter provincia = new SqlParameter("@PROV", prov);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, nom, dir, provincia);
        }

        public async Task EliminarSala(int id)
        {
            string sql = "SP_ELIMINARSALA @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);

        }
        public async Task EditarSala(int id, string nombre,
            string direccion, int prov)
        {
            string sql = "SP_UPDATE_SALA  @ID, @NOMBRE, @DIRECCION, @PROV";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter dir = new SqlParameter("@DIRECCION", direccion);
            SqlParameter provincia = new SqlParameter("@PROV", prov);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, dir, provincia);
        }

    }
}
