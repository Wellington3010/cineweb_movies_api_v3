using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Repositories
{
    public abstract class FilmeBaseRepository<T, Z> : BaseRepository<T, Z> where T : class
    {
        public abstract Task<T> FindByTitle(string title);
        public abstract Task<List<T>> FindByGenre(string genre);
    }
}
