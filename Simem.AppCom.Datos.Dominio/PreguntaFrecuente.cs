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
    [Table("PreguntaFrecuente", Schema = "simem")]
    public class PreguntaFrecuente
    {
        [Key()]
        public int IdPreguntaFrecuente { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        [NotMapped]
        public List<Etiqueta> Etiquetas { get; set; } = new();
    }
}
