using gardenrosesmvc2.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace gardenrosesmvc2.Controllers
{
    public class DetallePedidoController : Controller
    {
        private readonly string baseUrl = "https://localhost:44358/api/detallepedido";

        // GET: DetallePedido
        public async Task<ActionResult> Index()
        {
            List<DetallePedido> detalles = new List<DetallePedido>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    detalles = JsonConvert.DeserializeObject<List<DetallePedido>>(data);
                }
            }

            return View(detalles);
        }

        // POST: DetallePedido/Crear
        [HttpPost]
        public async Task<ActionResult> Crear(DetallePedido detalle)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(detalle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(baseUrl, content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error en la API al crear detalle");
            }
        }

        // POST: DetallePedido/Editar
        [HttpPost]
        public async Task<ActionResult> Editar(DetallePedido detalle)
        {
            if (!ModelState.IsValid || detalle == null)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(detalle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{baseUrl}/{detalle.IdDetallePedido}", content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error en la API al actualizar detalle");
            }
        }

        // POST: DetallePedido/Eliminar
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
