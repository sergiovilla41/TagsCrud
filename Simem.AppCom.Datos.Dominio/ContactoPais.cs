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
    [Table("ContactoPais", Schema = "simem")]
    public class ContactoPais
    {
        [Key()]
        public Guid Id { get; set; }
        public string? Pais { get; set; }
        public int IdPais { get; set; }
    }
}
