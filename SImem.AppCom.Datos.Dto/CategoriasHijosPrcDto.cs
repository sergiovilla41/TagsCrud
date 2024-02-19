using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SImem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class CategoriasHijosPrcDto
    {
        public Guid? Id { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public bool estado { get; set; }
        public string? icono { get; set; }
        public int conjuntoDato { get; set; }
        public int Nivel { get; set; }
        public int? NivelSuperior { get; set; }
        public Guid? CategoriaId { get; set; }
        public int ordenCategoria { get; set; }
    }
}

