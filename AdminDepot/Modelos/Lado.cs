using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminDepot.Modelos
{
    public class Lado
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Compartimiento> Compartimientos { get; set; }

    }
}