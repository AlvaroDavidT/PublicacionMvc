using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicacionMvc.Models
{
    public class Comentario
    {
        public Guid Id { get; set; }
        public string Contenido { get; set; }  
        public DateTime FechaComentario { get; set; }
        public Guid PublicacionId { get; set; }
    }
}