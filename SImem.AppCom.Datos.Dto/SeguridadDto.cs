using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class SeguridadDto
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? token { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string? usuarioID { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class Credencial
    {
        public string? correo { get; set; }
        public string? nombre { get; set; }

    }
    [ExcludeFromCodeCoverage]
    public class SecrectKey
    {
        public string? Token { get; set; }
        public string? Observacion { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class Token
    {
        public string? IdToken { get; set; }
    }
}