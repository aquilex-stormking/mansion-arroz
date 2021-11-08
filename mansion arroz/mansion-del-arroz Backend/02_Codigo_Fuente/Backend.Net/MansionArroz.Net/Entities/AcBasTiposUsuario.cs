using System;
using System.Collections.Generic;

#nullable disable

namespace MansionArroz.Net
{
    public partial class AcBasTiposUsuario
    {
        public AcBasTiposUsuario()
        {
            AcBasUsuarios = new HashSet<AcBasUsuario>();
        }

        public long TipoUsuarioId { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }

        public virtual ICollection<AcBasUsuario> AcBasUsuarios { get; set; }
    }
}
