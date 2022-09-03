using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminDepot.Modelos
{
    public class Producto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoProducto { get; set; }
        public int Cantidad { get; set; }
    }
}