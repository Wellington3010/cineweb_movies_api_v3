using cineweb_movies_api.Context;
using cineweb_movies_api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Repositories
{
    public class ClienteRepository : ClienteBaseRepository<Cliente, int, string>
    {
        private readonly ApplicationContext _applicationContext;
        public ClienteRepository(ApplicationContext appplicationContext)
        {
            _applicationContext = appplicationContext;
        }
        public override async Task AddItem(Cliente item)
        {
            _applicationContext.Clientes.Add(item);
            await _applicationContext.SaveChangesAsync();
        }

        public override async Task<List<Cliente>> FindAll()
        {
            return await _applicationContext.Clientes.ToListAsync();
        }

        public override async Task<Cliente> FindByCPF(string CPF)
        {
            return await _applicationContext.Clientes.FirstOrDefaultAsync(x => x.CPF == CPF);
        }

        public override Task<Cliente> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Cliente> ListItems()
        {
            throw new NotImplementedException();
        }

        public override async Task RemoveById(int id)
        {
            _applicationContext.Clientes.Remove(await FindById(id));
            await _applicationContext.SaveChangesAsync();
        }

        public override async Task Update(Cliente item)
        {
            _applicationContext.Clientes.Update(item);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
