using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.DTO
{
    public class CreateMovieDTO
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public bool HomeMovie { get; set; }

        [Required]
        [RegularExpression(@"^data:image\/[a-z]+;base64,", ErrorMessage = "A imagem do poster deve estar no formato webp do tipo base64")]
        public string Poster { get; set; }

        [Required]
        public string Sinopse { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}