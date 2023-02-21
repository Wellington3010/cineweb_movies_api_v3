using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Repositories
{
    public abstract class PedidoBaseRepository<T, Z> : BaseRepository<T, Z> where T : class
    {
        public abstract Task<List<T>> FindPedidosByCliente(Z idCliente);
        public abstract Task<bool> UpdatePedido(T pedido);
    }
}
