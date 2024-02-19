using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{

    [ExcludeFromCodeCoverage]
    public class CategoriasHijosPrcResult
        {
            public Guid? Id { get; set; }
            public string? titulo { get; set; }
            public string? descripcion { get; set; }
            public bool estado { get; set; }
            public string? icono { get; set; }
            public int conjuntoDato { get; set; }
            public int Nivel { get; set; }
            public int? NivelSuperior { get; set; }
            public Guid? IdCategoria { get; set; }
            public int ordenCategoria { get; set; }
        }

    }

