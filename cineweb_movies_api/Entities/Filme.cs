using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Entities
{
    [Table("filme")]
    public class Filme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Genero { get; set; }

        public byte[] Poster { get; set; }

        [Required]
        public string Sinopse { get; set; }

        [Required]
        public bool HomeMovie { get; set; }

        [Required]
        public bool Active { get; set; }

        public Ingresso Ingresso { get; set; }
    }
}
