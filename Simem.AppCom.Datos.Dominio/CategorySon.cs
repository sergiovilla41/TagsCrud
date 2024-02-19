using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    public class CategorySon
    {
        public Guid Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set;}
        public bool Estado { get; set; }
        public string? Icono { get; set; }
        public int ConjuntoDato { get; set; }
        public int? Nivel { get; set; }
        public int? NivelSuperior { get; set; }
        public Guid? CategoriaId { get; set; }
        public int? OrdenCategoria { get; set; }
    }
}
