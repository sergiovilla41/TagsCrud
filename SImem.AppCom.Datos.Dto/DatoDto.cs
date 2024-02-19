using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class DatoDto
    {
        [Key()]
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string? Titulo { get; set; }
        public string? SubTitulo { get; set; }
        public string? Descripcion { get; set; }
        public Guid? TipoVistaID { get; set; }
        public bool? Estado { get; set; }
        public string? Tipo { get; set; }
        public string? EntidadOrigen { get; set; }
        public Guid? CategoriaID { get; set; }
        public Guid? IdPeriodicidad { get; set; }
        public string? Etiqueta { get; set; }
        public string? Granularidad { get; set; }
        public string? Periocidad { get; set; }
        public string? UsuarioInsercion { get; set; }
        public DateTime FechaInsercion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public int ContadorVistas { get; set; }
        public int ContadorDescargas { get; set; }
        public bool Privacidad { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? IdDataSet { get; set; }
        public string? ExtencionArchivo { get; set; }
        public DateTime? UltimaFechaIndexado { get; set; }
        public DateTime? UltimaFechaActualizado { get; set; }
        public TipoVistaDto? TipoVista { get; set; }
        public CategoriaDto? Categoria { get; set; }
        public DataSetColumnDto? ColumnaDestino { get; set; }

        public List<EnlaceDto>? LstEnlace { get; set; }
        public string? URLDataHistorica { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? NovedadesCount { get; set; } = 0;
        public int? RecentNews { get; set; } = 0;

        public DatoDto()
        {
            TipoVista = new TipoVistaDto();
            Categoria = new CategoriaDto();
           
            LstEnlace = new List<EnlaceDto>();
        }
    }

    [ExcludeFromCodeCoverage]
    public class LastDataUpdatedDto
    {
        public Guid Id { get; set; }
        public string? Titulo { get; set; }
        public string? FechaModificacion { get; set; }
        public List<EnlaceDto>? Etiqueta { get; set; }
        public string? Icono { get; set; }
        public string? Subtitulo { get; set; }
    }


}
