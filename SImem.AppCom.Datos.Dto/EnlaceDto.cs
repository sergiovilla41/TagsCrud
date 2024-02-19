using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class EnlaceDto
    {
        public Guid? Id { get; set; }
        public string? Titulo { get; set; }
        public bool Estado { get; set; }      
       


    }
}
    