using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminDepot.Modelos
{
    public class Pasillo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Lado LadoA { get; set; } = new Lado();
        public Lado LadoB { get; set; } = new Lado();

    }
}