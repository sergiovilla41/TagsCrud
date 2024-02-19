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
    [Table("Archivo", Schema = "dato")]
    public class Archivo
    {
        [Key()]
        public string? IdArchivo { get; set; }
        public string? TipoContenido { get; set; }
        public string? RutaContenedora { get; set; }
        public string? RutaAbsolutaDirectorioPadre { get; set; }
        public string? NombreArchivo { get; set; }
        public string? NombreContenedor { get; set; }
        public long Tamanio { get; set; }
        public string? ModificadoPor { get; set; }
        public string? CreadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Ruta { get; set; }
        public DateTime? RutaFechaExpiracion { get; set; }
        public Guid? IdConfiguracionGeneracionArchivo { get; set; }
        public DateTime FechaIndexado { get; set; }
        public int FilasTotales { get; set; }
    }
}
