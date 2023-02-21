using cineweb_movies_api.Context;
using cineweb_movies_api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Repositories
{
    public class FilmeRepository : FilmeBaseRepository<Filme, Guid>
    {
        private ApplicationContext _movieContext { get; set; }
        public FilmeRepository(ApplicationContext movieContext)
        {
            _movieContext = movieContext;
        }

        public override async Task AddItem(Filme item)
        {
            _movieContext.Filmes.Add(item);
            await _movieContext.SaveChangesAsync();
        }

        public override async Task<Filme> FindById(Guid id)
        {
            return await _movieContext.Filmes.Include(x => x.Ingresso).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public override IQueryable<Filme> ListItems()
        {
            return _movieContext.Filmes.Include(x => x.Ingresso).AsQueryable();
        }

        public override async Task RemoveById(Guid id)
        {
            var filme = _movieContext.Filmes.Where(x => x.Id == id).FirstOrDefault();
            _movieContext.Filmes.Remove(filme);
            await _movieContext.SaveChangesAsync();
        }

        public override async Task Update(Filme item)
        {
            _movieContext.Filmes.Update(item);
            await _movieContext.SaveChangesAsync();
        }

        public override async Task<List<Filme>> FindByGenre(string genre)
        {
            return await _movieContext.Filmes.Include(x => x.Ingresso).Where(x => x.Genero == genre).ToListAsync();
        }

        public override async Task<Filme> FindByTitle(string title)
        {
            return await _movieContext.Filmes.Include(x => x.Ingresso).Where(x => x.Titulo == title).FirstOrDefaultAsync();
        }

        public override async Task<List<Filme>> FindAll()
        {
            return await _movieContext.Filmes.Include(x => x.Ingresso).ToListAsync();
        }
    }
}
