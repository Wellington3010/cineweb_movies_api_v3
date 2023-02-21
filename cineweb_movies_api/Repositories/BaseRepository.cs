using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Repositories
{
    public abstract class BaseRepository<T, Z>  where T : class
    {
        public abstract Task<T> FindById(Z id);
        public abstract IQueryable<T> ListItems();

        public abstract Task AddItem(T item);

        public abstract Task RemoveById(Z id);

        public abstract Task Update(T item);

        public abstract Task <List<T>> FindAll();

    }
}
