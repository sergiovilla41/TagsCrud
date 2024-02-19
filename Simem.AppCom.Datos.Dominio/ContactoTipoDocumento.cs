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
    [Table("ContactoTipoDocumento", Schema = "simem")]
    public class ContactoTipoDocumento
    {
        [Key()]
        public Guid Id { get; set; }
        public string? TipoDocumento { get; set; }
        public string? CodigoTipoDocumento { get; set; }
    }
}
