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
    [Table("novedadEtiqueta", Schema = "simem")]
    public class NovedadEtiqueta
    {
        [Key()]
        public Guid IdNovedadEtiqueta { get; set; }
        public Guid IdEtiqueta { get; set;}
        public Guid IdNovedad { get; set; }
        [ForeignKey("IdNovedad")]
        public Novedad? Novedad { get; set; }
        [ForeignKey("IdEtiqueta")]
        public Etiqueta? Etiqueta { get; set; }

    }
}
