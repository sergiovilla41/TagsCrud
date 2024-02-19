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
    [ExcludeFromCodeCoverage]
    [Table("EnlaceInteres", Schema = "simem")]
    public class EnlaceInteres
    {
        [Key()]
        public int IdEnlaceInteres { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Enlace { get; set; }
        public string? Icono { get; set; }
        public bool Estado { get; set; }
        [NotMapped]
        public List<Etiqueta>? Etiquetas { get; set; } =  new ();
    }
}
