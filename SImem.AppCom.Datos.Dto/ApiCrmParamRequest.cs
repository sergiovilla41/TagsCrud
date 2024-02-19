using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public  class ApiCrmParamRequest
    {
        public string? session { get; set; }
        public string? module_name { get; set; }
        public string? start { get; set; }
        public string? end { get; set; }
        public string? recept_start { get; set; }
        public string? recept_end { get; set; }
        public string? status { get; set; }
        public string? type { get; set; }
        public string? window { get; set; }
        public string? case_number { get; set; }
        public string? response { get; set; }
        public string? reception { get; set; }
        public string? response_start { get; set; }
        public string? response_end { get; set; }
        public string? modified_start { get; set; }
        public string? modified_end { get; set; }
        public string? limit { get; set; }
        public string? offset { get; set; }
    }
}
