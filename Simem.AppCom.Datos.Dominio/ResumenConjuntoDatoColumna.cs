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
    [Table("ResumenConjuntoDatoColumna", Schema = "simem")]
    public class ResumenConjuntoDatoColumna
    {
        [Key()]
        public Guid IdResumenConjuntoDatoColumna { get; set; }
        public Guid IdResumenConjuntoDato { get; set; }
        public string? Nombre { get; set; }
        public string? TipoDato { get; set; }
        public bool? Estado { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int OrdenColumna { get; set; }
        [ForeignKey("IdResumenConjuntoDato")]
        public ResumenConjuntoDato ResumenConjuntoDato { get; set; } = new();
    }
}
