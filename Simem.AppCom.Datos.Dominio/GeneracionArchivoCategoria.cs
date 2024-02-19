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
    public class GeneracionArchivoCategoria
    {
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
        public DateTime? ValorDeltaInicial { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? ValorDeltaFinal { get; set; }
        [ExcludeFromCodeCoverage]
        public  Int16? OrderPeriodicidad { get; set; }
        [ExcludeFromCodeCoverage]
        public Guid? IdPeriodicidad { get; set; }
        [ExcludeFromCodeCoverage]
        public int? IntervaloPeriodicidad { get; set; }
        [ExcludeFromCodeCoverage]
        public string? Titulo { get; set; }
        [ExcludeFromCodeCoverage]
        public string? TituloCategoriaNivel1 { get; set; }
        [ExcludeFromCodeCoverage]
        public Guid? IdCategoria { get; set; }
        [ExcludeFromCodeCoverage]
        [ForeignKey("idCategoria")]
        public Categoria? Categoria { get; set; }
        [ExcludeFromCodeCoverage]
        public Guid? IdGranularidad { get; set; }
        [ExcludeFromCodeCoverage]
        public Guid? IdTipoVista{ get; set; }
        [ExcludeFromCodeCoverage]
        [ForeignKey("IdTipoVista")]
        public TipoVista? TipoVista { get; set; }
        [ExcludeFromCodeCoverage]
        public bool? Privacidad { get; set; }
        [ExcludeFromCodeCoverage]
        public string? EntidadOrigen { get; set; }
        [ExcludeFromCodeCoverage]
        public bool? Estado { get; set; }
        [ExcludeFromCodeCoverage]
        public string? UsuarioInsercion { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? FechaInsercion { get; set; }
        [ExcludeFromCodeCoverage]
        public string? UsuarioModificacion { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? FechaModificacion { get; set; }
        [ExcludeFromCodeCoverage]
        public string? Descripcion { get; set; }
        [ExcludeFromCodeCoverage]
        public string? IdDataSet { get; set; }
        [ExcludeFromCodeCoverage]
        public int? ContadorVistas { get; set; }
        [ExcludeFromCodeCoverage]
        public int? ContadorDescargas { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? UltimaFechaIndexado { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? FechaCreacion { get; set; }
        [ExcludeFromCodeCoverage]
        public DateTime? FechaActualizacion { get; set; }
        [ExcludeFromCodeCoverage]
        public Guid? IdCategoriaNivel1 { get; set; }
        public ICollection<Granularidad>? Granularidad { get; set; }
        [ExcludeFromCodeCoverage]
        public ICollection<ConfiguracionPeriodicidad>? Periodicidad { get; set; }
        [ExcludeFromCodeCoverage]
        public ICollection<ColumnaDatoConjuntoDato>? ColumnaDatoConjuntoDato { get; set; }
    }
}
