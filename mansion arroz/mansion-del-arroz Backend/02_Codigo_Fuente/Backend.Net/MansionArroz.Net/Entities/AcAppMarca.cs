using System;
using System.Collections.Generic;

#nullable disable

namespace MansionArroz.Net
{
    public partial class AcAppMarca
    {
        public AcAppMarca()
        {
            AcAppProductos = new HashSet<AcAppProducto>();
        }

        public long MarcaId { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }

        public virtual ICollection<AcAppProducto> AcAppProductos { get; set; }
    }
}
