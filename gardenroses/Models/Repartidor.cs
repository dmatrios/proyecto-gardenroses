using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gardenroses.Models
{
    public class Repartidor
    {
        public int idRepartidor { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string dni { get; set; }
        public string celular { get; set; }
        public string licenciaConducir { get; set; }
    }
}