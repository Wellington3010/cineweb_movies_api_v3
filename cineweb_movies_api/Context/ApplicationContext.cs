using cineweb_movies_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace cineweb_movies_api.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public virtual DbSet<Filme> Filmes { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }

        public virtual DbSet<Pedido> Pedidos { get; set; }

        public virtual DbSet<Ingresso> Ingressos { get; set; }
    }
}
