using gardenrosesmvc2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace gardenrosesmvc2.Controllers
{
    public class ProductoController : Controller
    {
        private readonly string baseUrl = "https://localhost:44358/api/productos"; // cambia si tu ruta es diferente

        public async Task<ActionResult> Index()
        {
            List<Producto> productos = new List<Producto>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    productos = JsonConvert.DeserializeObject<List<Producto>>(data);
                }
            }

            return View(productos);
        }

        [HttpPost]
        public async Task<ActionResult> Crear(Producto producto)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(baseUrl, content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error al crear producto");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Editar(Producto producto)
        {
            if (!ModelState.IsValid || producto == null)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{baseUrl}/{producto.idProducto}", content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error al actualizar producto");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Eliminar(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                    return Json(new { success = true });

                return new HttpStatusCodeResult(response.StatusCode);
            }
        }
    }
}
