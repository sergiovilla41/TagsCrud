using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class InventarioVariablesResultDto
    {
        public List<ConfiguracionVariableDto> result { get; set; }  
        
        public int totalRecord { get; set; }

        public InventarioVariablesResultDto() 
        {
            result = new List<ConfiguracionVariableDto>();
            totalRecord = 0;
        }
    }
}
