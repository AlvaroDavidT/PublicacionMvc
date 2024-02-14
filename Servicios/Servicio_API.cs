using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PublicacionMvc.Models;

namespace PublicacionMvc.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private string _baseurl;
        private static string _token;
        private static string _usuario;
        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }


        public async Task<bool> Autenticar(Login login)
        {
            bool respuesta= false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("User/Loguear", content);
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LogueoRespuesta>(json_respuesta);
                _token = resultado.Token;
                _usuario=Convert.ToString(resultado.Id);
                respuesta =true;
            }
            return respuesta;
        }
        public async Task<List<PublicacionLista>> Lista()
        {
            List<PublicacionLista> ListaPublicaciones = new List<PublicacionLista>();
            var cliente = new HttpClient();
            
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);      
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync("Publicacion/Todas");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<List<PublicacionLista>>(json_respuesta);
                ListaPublicaciones = respuesta.ToList();
            }
            return ListaPublicaciones;
        }

        public async Task<PublicacionLista> Obtener(Guid idPublicacion)
        {
            PublicacionLista objeto = new PublicacionLista();
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);               
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"Publicacion/{idPublicacion}");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<PublicacionLista>(json_respuesta);
                objeto = respuesta;
            }
            return objeto;
        }

        public async Task<bool> Guardar(PublicacionLista publicacion)
        {
            bool respuesta = false;
            var cliente = new HttpClient();
            publicacion.IdUsuario = _usuario;
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);      
            cliente.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(publicacion), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("Publicacion/Crear", content);
            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Editar(PublicacionLista publicacion)
        {
            bool respuesta = false;
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            cliente.BaseAddress = new Uri(_baseurl);
            publicacion.IdUsuario = _usuario;
            var content = new StringContent(JsonConvert.SerializeObject(publicacion), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync("Publicacion/Actualizar", content);
            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(Guid idPublicacion)
        {
            bool respuesta = false;
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.DeleteAsync($"Publicacion/{idPublicacion}");
            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }


        public async Task<ComentarioRespuesta> ObtenerPublicacionComentario(Guid idPublicacion)
        {
            ComentarioRespuesta objeto = new();
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"Publicacion/Comentarios/{idPublicacion}");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<ComentarioRespuesta>(json_respuesta);
                objeto = respuesta;
            }
            return objeto;
        }

        public async Task<bool> RegistrarCuenta(Registrar registrar)
        {
            bool respuesta= false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(registrar), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("User", content);
            if (response.IsSuccessStatusCode)
            {
                respuesta =true;
            }
            return respuesta;
        }


    }
}