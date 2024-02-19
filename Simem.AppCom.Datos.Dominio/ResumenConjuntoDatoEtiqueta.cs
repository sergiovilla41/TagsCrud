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
    [Table("ResumenConjuntoDatoEtiqueta", Schema = "simem")]
    public class ResumenConjuntoDatoEtiqueta
    {
        [Key()]
        public Guid IdResumenConjuntoDatoEtiqueta { get; set; }
        public Guid IdResumenConjuntoDato { get; set; }
        public Guid IdEtiqueta { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
