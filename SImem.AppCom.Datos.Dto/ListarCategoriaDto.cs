using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ListarCategoriaDto
    {
        public Guid? Id { get; set; }
        public string? Titulo { get; set; }
        public string? Icono { get; set; }
        public bool Estado { get; set; }
        public string? Descripcion { get; set; }
        public int? ConjuntoDato { get; set; }
        public List<CategoryHijosDto> ListaHijosDto { get; set; } = new List<CategoryHijosDto>();
    }
}
