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
    public class RepartidorController : Controller
    {
        private readonly string baseUrl = "https://localhost:44358/api/repartidores";

        // GET: Repartidor
        public async Task<ActionResult> Index()
        {
            List<Repartidor> repartidores = new List<Repartidor>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    repartidores = JsonConvert.DeserializeObject<List<Repartidor>>(data);
                }
            }

            return View(repartidores);
        }

        // POST: Repartidor/Crear
        [HttpPost]
        public async Task<ActionResult> Crear(Repartidor repartidor)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(repartidor);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(baseUrl, content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error en la API al crear repartidor");
            }
        }

        // POST: Repartidor/Editar
        [HttpPost]
        public async Task<ActionResult> Editar(Repartidor repartidor)
        {
            if (!ModelState.IsValid || repartidor == null)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(repartidor);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{baseUrl}/{repartidor.idRepartidor}", content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error en la API al actualizar repartidor");
            }
        }

        // POST: Repartidor/Eliminar
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
