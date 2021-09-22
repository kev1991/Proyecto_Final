using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Proyecto_Final.Models
{
    public class Base_Modelo_Paginador
    {
        public int Actual_Page { get; set; }
        public int Total { get; set; }
        public int Records_Page { get; set; }
        public RouteValueDictionary valueQueryString { get; set; }
    }
}