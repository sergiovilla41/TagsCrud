using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Base.Utils
{
    [ExcludeFromCodeCoverage]
    public static class ErrorMessages
    {
        public const string ERROR_FIELDS = "Error de formato en los campos";

        public const string ERROR_ARCHIVOS_FIELDS = "Error en los campos";

        public const string ERROR_ARCHIVOS_FILE_NOT_FOUND = "No se encontro archivo";

        public const string ERROR_ARCHIVOS_FILE_EXISTS = "El archivo ya existe";

    }
}
