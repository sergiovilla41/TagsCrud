using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class EstandarizacionRegistrosFiltroDto
    {
        public Guid? IdColumnaDestino { get; set; }
        public string? ValorObjetivo { get; set; }
    }
}
