using gardenroses.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace gardenroses.Controllers
{
    public class ProductoController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [HttpGet]
        [Route("api/productos")]
        public IHttpActionResult ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_MostrarProductos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productos.Add(new Producto
                    {
                        IdProducto = Convert.ToInt32(reader["idProducto"]),
                        Nombre = reader["nombre"].ToString(),
                        Descripcion = reader["descripcion"].ToString(),
                        Precio = Convert.ToDecimal(reader["precio"]),
                        Stock = Convert.ToInt32(reader["stock"]),
        
                    });
                }
                reader.Close();
            }

            return Ok(productos);
        }

        [HttpGet]
        [Route("api/productos/{id}")]
        public IHttpActionResult ObtenerProductoPorId(int id)
        {
            Producto producto = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_BuscarProductoPorID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    producto = new Producto
                    {
                        IdProducto = Convert.ToInt32(reader["idProducto"]),
                        Nombre = reader["nombre"].ToString(),
                        Descripcion = reader["descripcion"].ToString(),
                        Precio = Convert.ToDecimal(reader["precio"]),
                        Stock = Convert.ToInt32(reader["stock"]),
                    };
                }
                reader.Close();
            }

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        [Route("api/productos")]
        public IHttpActionResult RegistrarProducto(Producto p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_RegistrarProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", p.Descripcion);
                cmd.Parameters.AddWithValue("@precio", p.Precio);
                cmd.Parameters.AddWithValue("@stock", p.Stock);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Producto registrado correctamente.");
        }

        [HttpPut]
        [Route("api/productos/{id}")]
        public IHttpActionResult ActualizarProducto(int id, Producto p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", p.Descripcion);
                cmd.Parameters.AddWithValue("@precio", p.Precio);
                cmd.Parameters.AddWithValue("@stock", p.Stock);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Producto actualizado correctamente.");
        }

        [HttpDelete]
        [Route("api/productos/{id}")]
        public IHttpActionResult EliminarProducto(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_EliminarProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Producto eliminado correctamente.");
        }
    }
}
