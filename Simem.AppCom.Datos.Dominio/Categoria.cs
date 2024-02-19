using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    [Table("Categoria", Schema = "dato")]
    public class Categoria
    {
        [Key()]
        public Guid? Id { get; set; }
        public Guid? IdCategoria { get; set; }
        public string? Titulo { get; set; }
        public string? Icono { get; set; }
        public bool Estado { get; set; }
        public string? Descripcion { get; set; }
        public int? OrdenCategoria { get; set; }
        public bool privado { get; set; }
        public int? CantidadConjuntoDato { get; set; }
        public int CantidadDescarga { get; set; }
        public ICollection<GeneracionArchivo>? GeneracionArchivo { get; set; }
    }
}
