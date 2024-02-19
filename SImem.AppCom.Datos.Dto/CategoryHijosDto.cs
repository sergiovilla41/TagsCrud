using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class CategoryHijosDto
    {
        public Guid? Id { get; set; }
        public Guid? CategoriaID { get; set; }
        public string? Titulo { get; set; }
        public string? Icono { get; set; }
        public bool Estado { get; set; }
        public string? Descripcion { get; set; }
        public int? ConjuntoDato { get; set; }
    }
}
