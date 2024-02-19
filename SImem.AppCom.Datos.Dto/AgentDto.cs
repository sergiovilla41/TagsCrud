using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class AgentDto
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Icon { get; set; }
        public bool State { get; set; }
    }
}
