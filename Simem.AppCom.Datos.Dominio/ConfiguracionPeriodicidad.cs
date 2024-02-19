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
    [Table("Periodicidad", Schema = "configuracion")]
    public class ConfiguracionPeriodicidad
    {
        [Key()]
        public Guid IdPeriodicidad { get; set; }
        public string? Periodicidad { get; set; }
        public Int16 OrdenPeriodicidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        [ExcludeFromCodeCoverage]
        public ICollection<GeneracionArchivo>? GeneracionArchivo { get; set; }

    }
}
