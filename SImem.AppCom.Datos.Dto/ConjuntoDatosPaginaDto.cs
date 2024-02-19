using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ConjuntoDatosPaginaDto
    {
        public int totalFilas { get; set; }
        public List<DatoDto>? resultadoJson { get; set; }
    }
}
