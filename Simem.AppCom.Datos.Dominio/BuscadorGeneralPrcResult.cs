using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    public class BuscadorGeneralPrcResult
    {
        public int totalFilas { get; set; }
        public string? resultadoJson { get; set; }
        public string? Etiquetas { get; set; }
    }
}
