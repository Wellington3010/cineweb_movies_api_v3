using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cineweb_movies_api.Entities
{
    [Table("pedido")]
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid CodigoPedido { get; set; }

        [ForeignKey("filme")]
        public Guid FilmeId { get; set; }

        [NotMapped]
        public Filme Filme { get; set; }

        [ForeignKey("ingresso")]
        public int IdIngresso { get; set; }

        [NotMapped]
        public Ingresso Ingresso { get; set; }

        public decimal ValorTotal { get; set; }

        public int IdCliente { get; set; }
    }
}
