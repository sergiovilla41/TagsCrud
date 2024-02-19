using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    [Table("VariableGeneracionArchivos", Schema = "Configuracion")]
    public class ConfiguracionVariableGeneracionArchivo
    {
        [Key()]
        public Guid IdVariableGeneracionArchivos { get; set; }
        public Guid IdVariable { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
