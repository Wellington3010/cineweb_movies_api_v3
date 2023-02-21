using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Entities
{
    [Table("ingresso")]
    public class Ingresso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdIngresso { get; set; }

        [ForeignKey("filme")]
        public  Guid FilmeId { get; set; }
        
        public Filme Filme { get; set; }

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }
    }
}
