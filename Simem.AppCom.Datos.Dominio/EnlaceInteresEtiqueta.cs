using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{
    [Table("enlaceInteresEtiqueta", Schema = "simem")]
    public class EnlaceInteresEtiqueta
    {
        [Key]
        public Guid IdEnlaceInteresEtiqueta { get; set; }
        [ForeignKey("IdEnlaceInteres")]
        public EnlaceInteres? EnlaceInteres { get; set; }
        [ForeignKey("IdEtiqueta")]
        public Etiqueta? Etiqueta { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
