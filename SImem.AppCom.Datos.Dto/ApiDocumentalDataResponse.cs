using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApiDocumentalDataResponse
    {
        public string? statusCode { get; set; }
        public string? statusDesc { get; set; }
        public TypeRecords? records { get; set; }
        public string? addInfo { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TypeRecords
    {
        public string? numeroRadicado { get; set; }
        public string? fechaRadicacion { get; set; }
    }
}
