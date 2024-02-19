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
    [Table("Rol", Schema = "seguridad")]
    public class Rol
    {
        [Key()]
        public Guid Id { get; set; }
        public string NombreRol { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public  ICollection<UsuarioRol>? UsuarioRoles { get; set; }
        public  ICollection<RolConfiguracionGeneracionArchivos>? RolConfiguracionGeneracionArchivos { get; set; }

        public Rol()
        {
            NombreRol= string.Empty;
        }
    }
}
