using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicacionMvc.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }     
        public IList<Publicacion> Publicaciones { get; set; }
    }
}