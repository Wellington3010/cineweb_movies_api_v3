using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.DTO
{
    public class PedidoDTO
    {
        [Required]
        public List<string> Titulos { get; set; }

        [Required]
        public int ValorTotal { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string NomeCliente { get; set; }
    }
}
