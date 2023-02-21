using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.DTO
{
    public class MovieDTO
    {
        public string Id { get; set; }

        public string Titulo { get; set; }

        public DateTime Data { get; set; }

        public string Genero { get; set; }

        public bool HomeMovie { get; set; }

        public string Poster { get; set; }

        public string Sinopse { get; set; }

        public bool Active { get; set; }
        public int QuantidadeIngressos { get; set; }

        public int Preco { get; set; }

    }
}
