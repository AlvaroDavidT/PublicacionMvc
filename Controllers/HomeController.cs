using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PublicacionMvc.Extensions;
using PublicacionMvc.Models;
using PublicacionMvc.Servicios;

namespace PublicacionMvc.Controllers;

public class HomeController : Controller
{
    private readonly IServicio_API _servicio_API;

    public HomeController(IServicio_API servicio_API)
    {
        _servicio_API = servicio_API;
    }

    public async Task<IActionResult> Publicacion(Guid idPublicacion)
    {
        PublicacionLista modelo_publicacion = new PublicacionLista();
        ViewBag.Accion = "Nueva Publicacion";

        if (idPublicacion != Guid.Empty)
        {
            modelo_publicacion = await _servicio_API.Obtener(idPublicacion);
            ViewBag.Accion = "Editar Publicacion";
        }
        return View(modelo_publicacion);
    }

    [HttpPost]
    public async Task<IActionResult> GuardarCambios(PublicacionLista ob_Producto)
    {
        bool respuesta;
        if (ob_Producto.id == Guid.Empty)
        {
            respuesta = await _servicio_API.Guardar(ob_Producto);
        }
        else
        {
            respuesta = await _servicio_API.Editar(ob_Producto);
        }
        if (respuesta)
            return RedirectToAction("ListaPublicaciones");
        else
            return NoContent();


    }

    [HttpGet]
    public async Task<IActionResult> Eliminar(Guid idPublicacion)
    {
        var respuesta = await _servicio_API.Eliminar(idPublicacion);

        if (respuesta)
            return RedirectToAction("ListaPublicaciones");
        else
            return NoContent();
    }

    public async Task<IActionResult> PublicacionDetalle(Guid idPublicacion)
    {
        Usuario usuario = new Usuario();
        if (idPublicacion != Guid.Empty)
        {
            var objeto = await _servicio_API.ObtenerPublicacionComentario(idPublicacion);
            usuario = objeto.usuario;
        }
        return View(usuario);
    }

    public async Task<IActionResult> Index(Guid idPublicacion)
    {
        Usuario usuario = new Usuario();
        if (idPublicacion != Guid.Empty)
        {
            var objeto = await _servicio_API.ObtenerPublicacionComentario(idPublicacion);
            usuario = objeto.usuario;
        }
        return View(usuario);
    }

    public async Task<IActionResult> ListaPublicaciones(Login login)
    {
         List<PublicacionLista> lista = new List<PublicacionLista>();

        if(HttpContext.Session.GetObject<bool>("SESSION"))
        {
            lista = await _servicio_API.Lista();
        }else
        {
            if(await _servicio_API.Autenticar(login)){
                lista = await _servicio_API.Lista();
                HttpContext.Session.SetObject("SESSION", true);
            }  else {
                return RedirectToAction ("Login");
            }  
                  
        }
        return View(lista);
    }


    public async Task<IActionResult> RegistrarCuenta(Registrar registrar)
    {
        if (registrar != null)
        {
            await _servicio_API.RegistrarCuenta(registrar);
        }
        
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Login()
    {
        return View();
    }
    public async Task<IActionResult> Registrar()
    {
        return View();
    }

}
