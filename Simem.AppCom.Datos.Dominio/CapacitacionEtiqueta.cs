using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    [Table("capacitacionEtiqueta", Schema = "simem")]
    public class CapacitacionEtiqueta
    {
        [Key()]
        public Guid IdCapacitacionEtiqueta { get; set; }
        [ForeignKey("IdCapacitacion")]
        public Capacitacion? Capacitacion { get; set; }
        [ForeignKey("IdEtiqueta")]
        public Etiqueta? Etiqueta { get; set; }
        public Guid IdEtiqueta { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
