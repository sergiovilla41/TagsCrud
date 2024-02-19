using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApiCrmLoginParamRequest
    {
        public string? user_name { get; set; }
        public string? password { get; set; }
        public string? encryption { get; set; }
    }
}
