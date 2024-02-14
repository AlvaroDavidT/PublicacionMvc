using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicacionMvc.Models
{
    public class LogueoRespuesta
    {
        public string Token { set; get; }
        public Guid Id { set; get; }
        public string Nombre { set; get; }
        public string Email { set; get; }
    }
}