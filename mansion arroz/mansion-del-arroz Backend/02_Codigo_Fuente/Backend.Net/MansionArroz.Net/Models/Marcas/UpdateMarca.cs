﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models.Marcas
{
    public class UpdateMarca
    {
        public long marcaId { get; set; }

        public string descripcion { get; set; }

        public bool activo { get; set; }

        public string usuarioAuditoria { get; set; }
    }
}
