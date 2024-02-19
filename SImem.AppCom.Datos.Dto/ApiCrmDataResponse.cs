using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApiCrmDataResponse
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public CasesList[]? data { get; set; }
    }
}
