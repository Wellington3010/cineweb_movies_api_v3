using cineweb_movies_api.Context;
using cineweb_movies_api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Repositories
{
    public class IngressoRepository : IngressoBaseRepository<Ingresso, int, Guid>
    {
        private ApplicationContext _applicationContext { get; set; }

        public IngressoRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public override async Task<Ingresso> FindById(int id)
        {
            return await _applicationContext.Ingressos.Where(x => x.IdIngresso == id).FirstOrDefaultAsync();
        }

        public override IQueryable<Ingresso> ListItems()
        {
            return _applicationContext.Ingressos.AsQueryable();
        }

        public override async Task AddItem(Ingresso item)
        {
            _applicationContext.Ingressos.Add(item);
            await _applicationContext.SaveChangesAsync();   
        }

        public override async Task RemoveById(int id)
        {
            var ingresso = _applicationContext.Ingressos.Where(x => x.IdIngresso == id).FirstOrDefault();
            _applicationContext.Ingressos.Remove(ingresso);
            await _applicationContext.SaveChangesAsync();
        }

        public override async Task Update(Ingresso item)
        {
            _applicationContext.Ingressos.Update(item);
            await _applicationContext.SaveChangesAsync();
        }
      
        public override Task<List<Ingresso>> FindAll()
        {
            throw new NotImplementedException();
        }

        public override async Task<Ingresso> ListarIngressosPorFilme(Guid FilmeId)
        {
            return await _applicationContext.Ingressos.Where(x => x.FilmeId == FilmeId).FirstOrDefaultAsync();
        }
    }
}
