using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{

    [ExcludeFromCodeCoverage]
    public class BuscadorGeneralConjuntoDatosPrcResult
    {
        public int totalFilas { get; set; }
        public string? etiquetas { get; set; }
        public string? variables { get; set; }
        public string? resultadoJson { get; set; }
    }
}
