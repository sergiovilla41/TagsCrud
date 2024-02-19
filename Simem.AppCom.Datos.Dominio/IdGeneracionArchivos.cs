using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    public class IdGeneracionArchivos
    {
        [Key()]
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
    }
}
