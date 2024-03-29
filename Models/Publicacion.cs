using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicacionMvc.Models
{
    public class Publicacion
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public Guid UsuarioId { get; set; }
        public IList<Comentario> Comentarios { get; set; }
    }
}