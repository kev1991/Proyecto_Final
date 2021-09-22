using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Final.Models
{
    public class Cliente_Index:Base_Modelo_Paginador
    {
        public List<cliente> Clientes { get; set; }
    }
}