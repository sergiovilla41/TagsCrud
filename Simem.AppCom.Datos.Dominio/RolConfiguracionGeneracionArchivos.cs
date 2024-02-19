using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    [Table("RolConfiguracionGeneracionArchivos", Schema = "seguridad")]
    public class RolConfiguracionGeneracionArchivos
    {
        [Key]
        public Guid IdRolConfiguracionGeneracionArchivos { get; set; }
        public Guid IdRol { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }

        [ForeignKey("IdRol")]
        public  Rol? Rol { get; set; }

        [ForeignKey("IdConfiguracionGeneracionArchivos")]
        public  GeneracionArchivo? GeneracionArchivo { get; set; }
    }
}
