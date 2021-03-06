using System;
using System.Collections.Generic;

#nullable disable

namespace MansionArroz.Net
{
    public partial class AcAppCliente
    {
        public AcAppCliente()
        {
            AcAppVenta = new HashSet<AcAppVenta>();
        }

        public long ClienteId { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }

        public virtual ICollection<AcAppVenta> AcAppVenta { get; set; }
    }
}
