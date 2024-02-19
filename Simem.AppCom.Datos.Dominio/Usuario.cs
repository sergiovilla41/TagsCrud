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
    [Table("Usuario", Schema = "usuario")]
    public class Usuario
    {
        [Key()]
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioCreadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificadoPor { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public  ICollection<UsuarioRol>? UsuarioRoles { get; set; }

        public Usuario()
        {
            Nombre=string.Empty;
            Correo=string.Empty;
        }

    }
}
