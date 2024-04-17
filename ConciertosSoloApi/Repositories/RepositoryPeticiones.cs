using ConciertosSoloApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Repositories
{
    public class RepositoryPeticiones
    {
        private ConciertosSoloContext context;

        public RepositoryPeticiones(ConciertosSoloContext context)
        {
            this.context = context;
        }

        public async Task<List<PeticionEvento>> GetPeticiones()
        {
            var consulta = from datos in this.context.Peticiones
                           select datos;
            return consulta.ToList();
        }

        public async Task InsertarPeticion
            (string nombre, int idprovincia, DateTime fecha, string telefono)
        {
            string sql = "SP_INSERT_PETICION @NOMBRE, @PROV, @FECHA, @TELF";

            SqlParameter pnom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pprov = new SqlParameter("@PROV", idprovincia);
            SqlParameter pfecha = new SqlParameter("@FECHA", fecha);
            SqlParameter ptelf = new SqlParameter("@TELF", telefono);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pnom, pprov, pfecha, ptelf);
        }

        public async Task<List<Peticion>> GetListaPeticiones()
        {
            var consulta = from datos in this.context.ListaPeticiones
                           select datos;
            return consulta.ToList();
        }

        public async Task EliminarPeticion(int id)
        {
            string sql = "SP_ELIMINARPETICION @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }
    }
}
