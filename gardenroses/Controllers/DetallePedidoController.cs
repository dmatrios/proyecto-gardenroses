using gardenroses.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace gardenroses.Controllers
{
    public class DetallePedidoController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [HttpGet]
        [Route("api/detallepedido")]
        public IHttpActionResult ObtenerDetalles()
        {
            List<DetallePedido> lista = new List<DetallePedido>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_MostrarDetallePedido", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new DetallePedido
                    {
                        IdDetallePedido = Convert.ToInt32(dr["idDetallePedido"]),
                        IdPedido = Convert.ToInt32(dr["idPedido"]),
                        IdProducto = Convert.ToInt32(dr["idProducto"]),
                        NombreProducto = dr["nombreProducto"].ToString(),
                        Cantidad = Convert.ToInt32(dr["cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(dr["precioUnitario"]),
                        Subtotal = Convert.ToDecimal(dr["subtotal"])
                    });
                }
            }

            return Ok(lista);
        }

        [HttpGet]
        [Route("api/detallepedido/{id}")]
        public IHttpActionResult ObtenerDetallePorId(int id)
        {
            DetallePedido detalle = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_BuscarDetallePedidoPorID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDetallePedido", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    detalle = new DetallePedido
                    {
                        IdDetallePedido = Convert.ToInt32(dr["idDetallePedido"]),
                        IdPedido = Convert.ToInt32(dr["idPedido"]),
                        IdProducto = Convert.ToInt32(dr["idProducto"]),
                        NombreProducto = dr["nombreProducto"].ToString(),
                        Cantidad = Convert.ToInt32(dr["cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(dr["precioUnitario"]),
                        Subtotal = Convert.ToDecimal(dr["subtotal"])
                    };
                }
            }

            if (detalle == null)
                return NotFound();

            return Ok(detalle);
        }

        [HttpPost]
        [Route("api/detallepedido")]
        public IHttpActionResult RegistrarDetalle(DetallePedido detalle)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_RegistrarDetallePedido", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPedido", detalle.IdPedido);
                cmd.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
                cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Detalle registrado correctamente.");
        }

        [HttpPut]
        [Route("api/detallepedido/{id}")]
        public IHttpActionResult ActualizarDetalle(int id, DetallePedido detalle)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarDetallePedido", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDetallePedido", id);
                cmd.Parameters.AddWithValue("@idPedido", detalle.IdPedido);
                cmd.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
                cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Detalle actualizado correctamente.");
        }

        [HttpDelete]
        [Route("api/detallepedido/{id}")]
        public IHttpActionResult EliminarDetalle(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_EliminarDetallePedido", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDetallePedido", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Detalle eliminado correctamente.");
        }
    }
}
