using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Repositories
{
    public abstract class IngressoBaseRepository<T, Z, Y> : BaseRepository<T, Z> where T : class
    {
        public abstract Task<T> ListarIngressosPorFilme(Y FilmeId);
    }
}
