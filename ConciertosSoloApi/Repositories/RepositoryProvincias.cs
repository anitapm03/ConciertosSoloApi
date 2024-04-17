using ConciertosSoloApi.Data;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Repositories
{
    public class RepositoryProvincias
    {
        private ConciertosSoloContext context;

        public RepositoryProvincias(ConciertosSoloContext context)
        {
            this.context = context;
        }
        public async Task<List<Provincia>> GetProvincias()
        {
            var consulta = from datos in this.context.Provincias
                           select datos;
            return consulta.ToList();
        }
    }
}
