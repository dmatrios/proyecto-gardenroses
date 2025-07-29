using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gardenrosesmvc2.Models
{
    public class DetallePedido
    {
        public int IdDetallePedido { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } // Este lo usas para mostrar el nombre
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}