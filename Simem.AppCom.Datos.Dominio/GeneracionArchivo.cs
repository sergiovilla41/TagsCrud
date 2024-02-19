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
    [Table("GeneracionArchivos", Schema = "Configuracion")]
    public class GeneracionArchivo
    {
        [Key()]
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        [ExcludeFromCodeCoverage]
        public string? Tema { get; set; }
        [ExcludeFromCodeCoverage]
        public string? NombreArchivoDestino { get; set; }
        [ExcludeFromCodeCoverage]
        public string? SelectXM { get; set; }
        [ExcludeFromCodeCoverage]
        public Guid? IdDuracionISO { get; set; }
        [ExcludeFromCodeCoverage]
        public string? NBSynapse { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime ValorDeltaInicial { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? ValorDeltaFinal { get; set; }
   
        [ExcludeFromCodeCoverage]
        public Guid? IdPeriodicidad { get; set; }
        [ExcludeFromCodeCoverage]
        public int? IntervaloPeriodicidad { get; set; }
        public string? Titulo { get; set; }
        public Guid? IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; }
        public Guid? IdGranularidad { get; set; }      
        public Guid? IdTipoVista { get; set; }
        [ForeignKey("IdTipoVista")]
        public TipoVista? TipoVista { get; set; }
        public bool Privacidad { get; set; }
        public string? EntidadOrigen { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioInsercion { get; set; }
        public DateTime? FechaInsercion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? Descripcion { get; set; }
        public string? IdDataSet { get; set; }
        public int? ContadorVistas { get; set; }
        public int? ContadorDescargas { get; set; }
        public DateTime FechaCreacion { get; set; }
        [ExcludeFromCodeCoverage, NotMapped]
        public string? ExtencionArchivo { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? FechaActualizacion { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? UltimaFechaIndexado { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? UltimaFechaActualizado { get; set; }
        public ICollection<Granularidad>? Granularidad { get; set; }
        [ExcludeFromCodeCoverage]
        public ICollection<ConfiguracionPeriodicidad>? Periodicidad { get; set; }
        [ExcludeFromCodeCoverage]
        public ICollection<ColumnaDatoConjuntoDato>? ColumnaDatoConjuntoDato { get; set; }
        [ForeignKey("IdColumnaDestino")]
        public ColumnaConjuntoDato? ColumnaDestino { get; set; }
        public string? URLDataHistorica { get; set; }
        public ICollection<RolConfiguracionGeneracionArchivos>? RolConfiguracionGeneracionArchivos { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaDescarga { get; set; }
        [ForeignKey("IdGeneracionArchivo")]
        public List<Novedad>? Novedades { get; set; }
        public Guid? IdCategoriaNivel1 { get; set; }
        [NotMapped]
        public Categoria? CategoriaNivel1 { get; set; } = null;
    }   
}