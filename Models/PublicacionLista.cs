using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicacionMvc.Models
{
    public class PublicacionLista
    {
        public Guid id { get; set; }
        public string titulo { get; set; }
        public string contenido { get; set; }
        public DateTime fechaPublicacion { get; set; }
        public string IdUsuario { get; set; }
    }
}
