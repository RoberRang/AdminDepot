using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminDepot.Modelos
{
    public class Administracion
    {
        public string Name { get; set; }
        public List<Deposito> Depositos { get; set; }
    }
}