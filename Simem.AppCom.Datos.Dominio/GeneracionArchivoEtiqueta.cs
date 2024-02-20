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
    [Table("GeneracionArchivoEtiqueta", Schema = "Configuracion")]
    public class GeneracionArchivoEtiqueta
    {
        [Key()]
        public Guid IdConfiguracionGeneracionArchivoxEtiqueta { get; set; }      
        public Guid IdConfiguracionGeneracionArchivo { get; set; }
        public Guid EtiquetaId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Etiqueta? Etiqueta { get; set; } = null;
       
    }
}
