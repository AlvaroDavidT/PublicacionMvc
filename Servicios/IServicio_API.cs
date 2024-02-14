using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicacionMvc.Models;

namespace PublicacionMvc.Servicios
{
    public interface IServicio_API
    {
        Task<List<PublicacionLista>> Lista();
        Task<PublicacionLista> Obtener(Guid idPublicacion);
        Task<bool> Guardar (PublicacionLista publicacion);
        Task<bool> Editar (PublicacionLista publicacion);
        Task<bool> Eliminar (Guid idPublicacion);
        Task<ComentarioRespuesta> ObtenerPublicacionComentario(Guid idPublicacion);
        Task <bool> Autenticar(Login login);
        Task <bool> RegistrarCuenta(Registrar registrar);
        

    }
}