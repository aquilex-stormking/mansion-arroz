using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models.Categorias
{
    public class UpdateCategoria
    {
        public long categoriaId { get; set; }

        public string descripcion { get; set; }

        public bool activo { get; set; }

        public string usuarioAuditoria { get; set; }

    }
}
