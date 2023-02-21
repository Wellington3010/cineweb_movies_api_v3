using cineweb_movies_api.Context;
using cineweb_movies_api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Repositories
{
    public class PedidoRepository : PedidoBaseRepository<Pedido, int>
    {
        private readonly ApplicationContext _applicationContext;
        public PedidoRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public override async Task AddItem(Pedido item)
        {
            _applicationContext.Pedidos.Add(item);
            await _applicationContext.SaveChangesAsync();
        }

        public override async Task<List<Pedido>> FindAll()
        {
            return await _applicationContext.Pedidos.ToListAsync();
        }

        public override  async Task<Pedido> FindById(int id)
        {
            return await _applicationContext.Pedidos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public override IQueryable<Pedido> ListItems()
        {
            throw new NotImplementedException();
        }

        public override async Task RemoveById(int id)
        {
           _applicationContext.Pedidos.Remove(await FindById(id));
           await _applicationContext.SaveChangesAsync();
        }

        public override async Task Update(Pedido item)
        {
            _applicationContext.Pedidos.Update(item);
            await _applicationContext.SaveChangesAsync();
        }

        public override async Task<bool> UpdatePedido(Pedido pedido)
        {
            try
            {
                _applicationContext.Pedidos.Update(pedido);
                await _applicationContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public override async Task<List<Pedido>> FindPedidosByCliente(int idCliente)
        {
            return await _applicationContext.Pedidos.Where(x => x.IdCliente == idCliente).ToListAsync();
        }
    }
}
