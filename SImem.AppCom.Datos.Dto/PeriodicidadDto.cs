using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class PeriodicidadDto
    {
        public Guid? IdPeriodicidad { get; set; }
        public string? Periodicidad { get; set; }
        public int OrdenPeriodicidad { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
