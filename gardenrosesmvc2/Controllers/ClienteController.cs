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
    public class ClienteController : Controller
    {
        private readonly string baseUrl = "https://localhost:44358/api/clientes";

        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    clientes = JsonConvert.DeserializeObject<List<Cliente>>(data);
                }
            }

            return View(clientes);
        }

        // POST: Cliente/Crear
        [HttpPost]
        public async Task<ActionResult> Crear(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(baseUrl, content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error en la API al crear cliente");
            }
        }

        // POST: Cliente/Editar
        [HttpPost]
        public async Task<ActionResult> Editar(Cliente cliente)
        {
            if (!ModelState.IsValid || cliente == null)
                return new HttpStatusCodeResult(400, "Datos inválidos");

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{baseUrl}/{cliente.idCliente}", content);

                if (response.IsSuccessStatusCode)
                    return new HttpStatusCodeResult(200);
                else
                    return new HttpStatusCodeResult(500, "Error en la API al actualizar cliente");
            }
        }

        // POST: Cliente/Eliminar
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
