using gardenroses.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace gardenroses.Controllers
{
    public class PedidoController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [HttpGet]
        [Route("api/pedidos")]
        public IHttpActionResult ObtenerPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_MostrarPedidos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    pedidos.Add(new Pedido
                    {
                        IdPedido = Convert.ToInt32(reader["idPedido"]),
                        IdCliente = Convert.ToInt32(reader["idCliente"]),
                        FechaPedido = Convert.ToDateTime(reader["fechaPedido"]),
                        FechaEntrega = Convert.ToDateTime(reader["fechaEntrega"]),
                        DireccionEntrega = reader["direccionEntrega"].ToString(),
                        Estado = reader["estado"].ToString(),
                        IdRepartidor = Convert.ToInt32(reader["idRepartidor"])
                    });
                }
            }

            return Ok(pedidos);
        }

        [HttpGet]
        [Route("api/pedidos/{id}")]
        public IHttpActionResult ObtenerPedidoPorId(int id)
        {
            Pedido pedido = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_BuscarPedidoPorID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Este es el parámetro que te falta y está causando el error:
                cmd.Parameters.AddWithValue("@idPedido", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    pedido = new Pedido
                    {
                        IdPedido = Convert.ToInt32(reader["idPedido"]),
                        IdCliente = Convert.ToInt32(reader["idCliente"]),
                        FechaPedido = Convert.ToDateTime(reader["fechaPedido"]),
                        FechaEntrega = Convert.ToDateTime(reader["fechaEntrega"]),
                        DireccionEntrega = reader["direccionEntrega"].ToString(),
                        Estado = reader["estado"].ToString(),
                        IdRepartidor = Convert.ToInt32(reader["idRepartidor"])
                    };
                }
            }

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }


        [HttpPost]
        [Route("api/pedidos")]
        public IHttpActionResult RegistrarPedido(Pedido p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_RegistrarPedido", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCliente", p.IdCliente);
                cmd.Parameters.AddWithValue("@fechaPedido", p.FechaPedido.Date); // solo fecha
                cmd.Parameters.AddWithValue("@fechaEntrega", p.FechaEntrega.Date); // solo fecha
                cmd.Parameters.AddWithValue("@direccionEntrega", p.DireccionEntrega);
                cmd.Parameters.AddWithValue("@estado", p.Estado);
                cmd.Parameters.AddWithValue("@idRepartidor", p.IdRepartidor);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Pedido registrado correctamente.");
        }

        [HttpPut]
        [Route("api/pedidos/{id}")]
        public IHttpActionResult ActualizarPedido(int id, Pedido p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarPedido", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idPedido", id);
                cmd.Parameters.AddWithValue("@idCliente", p.IdCliente);
                cmd.Parameters.AddWithValue("@fechaPedido", p.FechaPedido.Date);
                cmd.Parameters.AddWithValue("@fechaEntrega", p.FechaEntrega.Date);
                cmd.Parameters.AddWithValue("@direccionEntrega", p.DireccionEntrega);
                cmd.Parameters.AddWithValue("@estado", p.Estado);
                cmd.Parameters.AddWithValue("@idRepartidor", p.IdRepartidor);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Pedido actualizado correctamente.");
        }

        [HttpDelete]
        [Route("api/pedidos/{id}")]
        public IHttpActionResult EliminarPedido(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_EliminarPedido", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPedido", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Pedido eliminado correctamente.");
        }
    }
}
