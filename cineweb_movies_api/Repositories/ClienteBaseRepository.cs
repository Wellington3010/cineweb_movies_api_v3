using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Repositories
{
    public abstract class ClienteBaseRepository<T, Z, P> : BaseRepository<T, Z> where T : class
    {
        public abstract Task<T> FindByCPF(P CPF);
    }
}
