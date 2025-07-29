using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gardenrosesmvc2.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string Estado { get; set; }
        public int IdRepartidor { get; set; }
    }
}