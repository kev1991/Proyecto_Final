using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Final.Models
{
    public class Reporte
    {
        public String nombre_cliente { set; get; }
        public DateTime fecha_compra { set; get; }
        public int total_compra { set; get; }
        public String correo_cliente { set; get; }

    }
}