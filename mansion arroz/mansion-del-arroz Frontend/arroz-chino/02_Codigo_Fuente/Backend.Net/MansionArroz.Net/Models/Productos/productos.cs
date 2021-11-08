using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models
{
    public class productos
    {
        public long productoId { get; set; }
        public long marcaId { get; set; }
        public long proveedorId { get; set; }
        public long categoriaId { get; set; }
        public string descripcion { get; set; }
        public string observaciones { get; set; }
        public long cantidad { get; set; }
        public string valor { get; set; }
        public string valorimpuesto { get; set; }
        public bool activo { get; set; } 
    }
}
