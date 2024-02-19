using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Base.Utils
{
    [ExcludeFromCodeCoverage]
    public static class TipoSolicitud
    {
        public const string FallaTecnologica = "Falla tecnológica";
        public const string Preguntas = "Preguntas";
        public const string Oportunidades = "Oportunidades de mejora";
        public const string Solicitudes = "Solicitudes / Requerimientos";
    }
}
