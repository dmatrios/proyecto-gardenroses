﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gardenrosesmvc2.Models
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
    }
}
