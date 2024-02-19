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
    [Table("ResumenConjuntoDato", Schema = "simem")]
    public class ResumenConjuntoDato
    {
        [Key()]
        public Guid IdResumenConjuntoDato { get; set; }
        public string? IdDataset { get; set; }
        public string? Titulo { get; set; }
        public string? Subtitulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? ContadorVistas { get; set; }
        public int? ContadorDescargas { get; set; }
    }
}
