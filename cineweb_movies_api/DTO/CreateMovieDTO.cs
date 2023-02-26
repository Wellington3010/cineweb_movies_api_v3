using cineweb_movies_api.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.DTO
{
    public class CreateMovieDTO
    {
        [Required(ErrorMessage = "É obrigatório informar título do filme")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a data do filme")]
        [MovieDataValidation(ErrorMessage = "A data do filme sempre deve ser maior que a data atual")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o gênero do filme")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "É obrigatório informar se o filme estará ativo na página home")]
        public bool HomeMovie { get; set; }

        [RegularExpression(@"(data:image\/[+;webp[^;]+;base64[^']+)", ErrorMessage = "A imagem do poster deve estar no formato webp do tipo base64")]
        [Required(ErrorMessage = "É obrigatório o cadastro do poster")]
        public string Poster { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a sinopse do filme")]
        public string Sinopse { get; set; }

        [Required(ErrorMessage = "É obrigatório informar se o filme está ativo ou não")]
        public bool Active { get; set; }
    }
}