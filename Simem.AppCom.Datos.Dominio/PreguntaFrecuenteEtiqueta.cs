using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{
    [Table("preguntaFrecuenteEtiqueta", Schema = "Simem")]
    public class PreguntaFrecuenteEtiqueta
    {
        [Key]
        public Guid IdPreguntaFrecuenteEtiqueta { get; set; }
        [ForeignKey("IdPreguntaFrecuente")]
        public PreguntaFrecuente PreguntaFrecuente { get; set; } = new();
        [ForeignKey("IdEtiqueta")]
        public Etiqueta Etiqueta { get; set; } = new();
        public DateTime FechaCreacion { get; set;}

    }
}
