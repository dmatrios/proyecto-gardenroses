using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gardenrosesmvc2.Models
{
    public class Cliente
    {
        public int idCliente { get; set; } 
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
    }

}