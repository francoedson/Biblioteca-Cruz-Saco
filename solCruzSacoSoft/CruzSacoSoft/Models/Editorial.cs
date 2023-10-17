using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruzSacoSoft.Models
{
    public class Editorial
    {
        public string codEditorial { get; set; }

        public string descripcion { get; set; }

        public int intEstado { get; set; }

        public string vchEstado { get; set; }


        public string fechaRegistro { get; set; }
    }
}