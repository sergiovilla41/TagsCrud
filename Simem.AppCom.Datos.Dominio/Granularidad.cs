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
    [Table("Granularidad", Schema = "configuracion")]
    public class Granularidad
    {
        [Key()]          
        public Guid? IdGranularidad { get; set; }
        public string? NombreGranularidad { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        [ExcludeFromCodeCoverage]
        public ICollection<GeneracionArchivo>? GeneracionArchivo { get; set; }

    }
}
