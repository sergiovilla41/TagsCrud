using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SImem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ResumenInventarioVariableDto
    {
        public Guid IdVariable { get; set; }
        public string? IdDataset { get; set; }
        public string? NombreDataset { get; set; }
        public string? CodigoVariable { get; set; }
        public string? NombreVariable { get; set; }
        public string? Descripcion { get; set; }
        public string? UnidadMedida { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
