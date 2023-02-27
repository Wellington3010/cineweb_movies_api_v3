using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.DTO
{
    public class IngressoDTO
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        [Range(1, 99999, ErrorMessage = "O preço deve ser maior que zero")]
        public int Preco { get; set; }

        [Required]
        [Range(1,99999, ErrorMessage = "A quantidade de ingressos deve ser maior que zero")]
        public int? Quantidade { get; set; }
    }
}
