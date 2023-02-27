using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.DTO
{
    public class DeletarIngressoDTO
    {
        [Required]
        public string Titulo { get; set; }
    }
}
