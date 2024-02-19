using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{
    [Table("Capacitacion", Schema = "simem")]
    [ExcludeFromCodeCoverage]
    public class Capacitacion
    {
        [Key()]
        public Guid Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? EnlaceImagen { get; set; }
        public int? Orden { get; set; }
        public string? Tour { get; set; }
        [NotMapped]
        public List<Etiqueta> Etiquetas { get; set; } = new();
    }
}
