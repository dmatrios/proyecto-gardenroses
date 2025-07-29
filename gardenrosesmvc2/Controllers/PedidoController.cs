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
    public class PedidoController : Controller
    {
        private readonly string baseUrl = "https://localhost:44358/api/pedidos";

        // GET: Pedido
        public async Task<ActionResult> Index()
        {
            List<Pedido> pedidos = new List<Pedido>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    pedidos = JsonConvert.DeserializeObject<List<Pedido>>(data);
                }
            }

            return View(pedidos);
        }

        // POST: Pedido/Crear
        [HttpPost]
        public async Task<ActionResult> Crear(Pedido pedido)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            // Asegurar que las fechas no tengan horas (solo fecha)
            pedido.FechaPedido = pedido.FechaPedido.Date;
            pedido.FechaEntrega = pedido.FechaEntrega.Date;

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(pedido);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(baseUrl, content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error al crear el pedido en la API");
            }
        }

        // POST: Pedido/Editar
        [HttpPost]
        public async Task<ActionResult> Editar(Pedido pedido)
        {
            if (!ModelState.IsValid || pedido == null)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            // Asegurar que las fechas estén sin hora
            pedido.FechaPedido = pedido.FechaPedido.Date;
            pedido.FechaEntrega = pedido.FechaEntrega.Date;

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(pedido);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{baseUrl}/{pedido.IdPedido}", content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error al actualizar el pedido en la API");
            }
        }

        // POST: Pedido/Eliminar
        [HttpPost]
        public async Task<ActionResult> Eliminar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                    return Json(new { success = true });
                else
                    return new HttpStatusCodeResult((int)response.StatusCode, "Error al eliminar el pedido");
            }
        }

        // GET: Pedido/ObtenerPorId
        public async Task<JsonResult> ObtenerPorId(int id)
        {
            Pedido pedido = null;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    pedido = JsonConvert.DeserializeObject<Pedido>(data);
                }
            }

            return Json(pedido, JsonRequestBehavior.AllowGet);
        }
    }
}
