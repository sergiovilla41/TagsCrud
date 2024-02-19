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
    [Table("ContactoCodigo", Schema = "simem")]
    public class ContactoCodigo
    {
        [Key()]
        public Guid Id { get; set; }
        public string? CorreoUsuario { get; set; }
        public string? Codigo { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
