using System;
using System.Collections.Generic;

#nullable disable

namespace MansionArroz.Net
{
    public partial class AcAppUsuariosPorRole
    {
        public long UsuarioId { get; set; }
        public long RolId { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }

        public virtual AcAppRole Rol { get; set; }
        public virtual AcBasUsuario Usuario { get; set; }
    }
}
