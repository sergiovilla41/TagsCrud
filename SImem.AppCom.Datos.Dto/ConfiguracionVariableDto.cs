using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ConfiguracionVariableDto
    {
        public Guid IdVariable { get; set; }
        public string? CodVariable { get; set; }
        public string? NombreVariable { get; set; }
        public string? UnidadMedida { get; set; }
        public string? Descripcion { get; set; }
        public string? Proceso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? StrFecha { get; set; }
        public string? Vigencia { get; set; }
        public string? totalRecords { get; set; }

        public List<EtiquetasVariablesDto> Etiquetas { get; set; }

        public ConfiguracionVariableDto() {

            Etiquetas = new List<EtiquetasVariablesDto>();
        
        }


        public class EtiquetasVariablesDto
        {
            public Guid? id { get; set; }
            public string? Nombre { get; set; } 
        }
    }    
}
