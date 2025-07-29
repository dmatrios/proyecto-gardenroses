using gardenroses.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace gardenroses.Controllers
{
    public class RepartidorController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // GET: api/repartidores
        [HttpGet]
        [Route("api/repartidores")]
        public IHttpActionResult ObtenerRepartidores()
        {
            List<Repartidor> lista = new List<Repartidor>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_MostrarRepartidores", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Repartidor
                    {
                        idRepartidor = Convert.ToInt32(dr["idRepartidor"]),
                        nombres = dr["nombres"].ToString(),
                        apellidos = dr["apellidos"].ToString(),
                        dni = dr["dni"].ToString(),
                        celular = dr["celular"].ToString(),
                        licenciaConducir = dr["licenciaConducir"].ToString()
                    });
                }
            }

            return Ok(lista);
        }

        // GET: api/repartidores/{id}
        [HttpGet]
        [Route("api/repartidores/{id}")]
        public IHttpActionResult ObtenerPorId(int id)
        {
            Repartidor repartidor = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_BuscarRepartidorPorID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    repartidor = new Repartidor
                    {
                        idRepartidor = Convert.ToInt32(dr["idRepartidor"]),
                        nombres = dr["nombres"].ToString(),
                        apellidos = dr["apellidos"].ToString(),
                        dni = dr["dni"].ToString(),
                        celular = dr["celular"].ToString(),
                        licenciaConducir = dr["licenciaConducir"].ToString()
                    };
                }
            }

            if (repartidor == null)
                return NotFound();

            return Ok(repartidor);
        }

        // POST: api/repartidores
        [HttpPost]
        [Route("api/repartidores")]
        public IHttpActionResult Registrar([FromBody] Repartidor r)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_RegistrarRepartidor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombres", r.nombres);
                cmd.Parameters.AddWithValue("@apellidos", r.apellidos);
                cmd.Parameters.AddWithValue("@dni", r.dni);
                cmd.Parameters.AddWithValue("@celular", r.celular);
                cmd.Parameters.AddWithValue("@licencia", r.licenciaConducir);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Repartidor registrado exitosamente.");
        }

        // PUT: api/repartidores/{id}
        [HttpPut]
        [Route("api/repartidores/{id}")]
        public IHttpActionResult Actualizar(int id, [FromBody] Repartidor r)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarRepartidor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombres", r.nombres);
                cmd.Parameters.AddWithValue("@apellidos", r.apellidos);
                cmd.Parameters.AddWithValue("@dni", r.dni);
                cmd.Parameters.AddWithValue("@celular", r.celular);
                cmd.Parameters.AddWithValue("@licencia", r.licenciaConducir);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Repartidor actualizado correctamente.");
        }

        // DELETE: api/repartidores/{id}
        [HttpDelete]
        [Route("api/repartidores/{id}")]
        public IHttpActionResult Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_EliminarRepartidor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Repartidor eliminado correctamente.");
        }
    }
}
