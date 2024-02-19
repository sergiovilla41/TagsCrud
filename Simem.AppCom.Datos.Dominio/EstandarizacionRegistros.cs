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
    [Table("EstandarizacionRegistros", Schema = "Configuracion")]
    public class EstandarizacionRegistros
    {
        [Key()]
        public Guid IdConfiguracionEstandarizacionRegistros { get; set; }
        public Guid? IdColumnaDestino { get; set; }
        public string? ValorObjetivo { get; set; }
        public string? AtributoVariable { get; set; }
        public Guid? IdVariable { get; set; }
        public DateTime? FechaCreacion { get; set; }

    }
}
