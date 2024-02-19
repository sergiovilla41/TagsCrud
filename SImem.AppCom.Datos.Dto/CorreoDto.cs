using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class CorreoDto
    {
        public string? CorreoUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? CodigoVerificacion { get; set; }
    }
}
