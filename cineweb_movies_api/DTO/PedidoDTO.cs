using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.DTO
{
    public class PedidoDTO
    {
        public List<string> Titulos { get; set; }

        public int ValorTotal { get; set; }

        public string CPF { get; set; }

        public string NomeCliente { get; set; }
    }
}
