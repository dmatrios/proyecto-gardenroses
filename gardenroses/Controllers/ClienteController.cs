using gardenroses.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace gardenroses.Controllers
{
    public class ClienteController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        // GET: api/clientes
        [HttpGet]
        [Route("api/clientes")]
        public IHttpActionResult ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_MostrarClientes", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        idCliente = Convert.ToInt32(reader["idCliente"]),
                        nombres = reader["nombres"].ToString(),
                        apellidos = reader["apellidos"].ToString(),
                        direccion = reader["direccion"].ToString(),
                        telefono = reader["telefono"].ToString(),
                        correo = reader["correo"].ToString()
                    });
                }
            }

            return Ok(clientes);
        }

        [HttpGet]
        [Route("api/clientes/{id}")]
        public IHttpActionResult ObtenerClientePorId(int id)
        {
            Cliente cliente = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_BuscarClientePorID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cliente = new Cliente
                    {
                        idCliente = Convert.ToInt32(reader["idCliente"]),
                        nombres = reader["nombres"].ToString(),
                        apellidos = reader["apellidos"].ToString(),
                        direccion = reader["direccion"].ToString(),
                        telefono = reader["telefono"].ToString(),
                        correo = reader["correo"].ToString()
                    };
                }
            }

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }


        // POST: api/clientes
        [HttpPost]
        [Route("api/clientes")]
        public IHttpActionResult CrearCliente([FromBody] Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_RegistrarCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombres", cliente.nombres);
                cmd.Parameters.AddWithValue("@apellidos", cliente.apellidos);
                cmd.Parameters.AddWithValue("@direccion", cliente.direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.telefono);
                cmd.Parameters.AddWithValue("@correo", cliente.correo);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Cliente creado exitosamente.");
        }

        [HttpPut]
        [Route("api/clientes/{id}")]
        public IHttpActionResult ActualizarCliente(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("El objeto cliente enviado está vacío o mal formateado.");
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombres", cliente.nombres ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@apellidos", cliente.apellidos ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@direccion", cliente.direccion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@telefono", cliente.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@correo", cliente.correo ?? (object)DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Cliente actualizado correctamente.");
        }


        // DELETE: api/clientes/{id}
        [HttpDelete]
        [Route("api/clientes/{id}")]

        public IHttpActionResult EliminarCliente(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_EliminarCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Cliente eliminado correctamente.");
        }
    }
}
