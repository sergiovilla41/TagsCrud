using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class VariableDto
    {
        public Guid Id { get; set; }
        public string? Titulo { get; set; }
        public DateTime FechaInicio { get; set; }
        public string? UnidadMedida { get; set; }
        public string? Descripcion { get; set; }
        public string? StrFecha { get; set; }
    }
}
