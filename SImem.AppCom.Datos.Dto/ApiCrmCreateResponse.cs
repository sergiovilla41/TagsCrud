using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApiCrmCreateResponse
    {
        public string? id { get; set; }
        public string? case_number { get; set; }
        public string? name { get; set; }
        public Object? errors { get; set; }
    }
}
