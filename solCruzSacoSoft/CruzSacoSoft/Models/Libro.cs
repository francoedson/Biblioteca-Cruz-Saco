using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruzSacoSoft.Models
{
    public class Libro
    {
        public string codLibro { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public string categoria { get; set; }
        public string editorial { get; set; }
        public string ubicacion { get; set; }
        public int ejemplares{ get; set; }
        public string vchEstado { get; set; }
        public int intEstado { get; set; }
        public string fechaCreacion { get; set; }

    }
}