using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models.Ventas
{
    public class DetalleVenta
    {
        public long productoId { get; set; }
        public string ProductoDescricpcion { get; set; }
        public string cantidad { get; set; }
        public string valorUnitarioImpuesto { get; set; }
        public string valorUnitario { get; set; }
        public string total { get; set; }
    }
}
