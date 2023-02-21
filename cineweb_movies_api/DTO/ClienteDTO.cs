using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.DTO
{
    public class ClienteDTO
    {
        public int IdUsuario { get; set; }

        public string NomeCliente { get; set; }

        public List<PedidoDTO> Pedidos { get; set; }
    }
}
