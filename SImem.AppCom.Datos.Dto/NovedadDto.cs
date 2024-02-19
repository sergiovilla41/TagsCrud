using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class NovedadDto
    {
        public Guid Id { get; set; }
        public string? Titulo { get; set; }
        public string? SubTitulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Contenido { get; set; }
        public string? TipoNovedad { get; set; }
        public DateTime? FechaInsercion { get; set; }
        public int? OrdenNovedad { get; set; }
        public string? autor { get; set; }
        public bool estado { get; set; }
        public DateTime? fechaPublicacion { get; set; }
        public string? enlaceInteres { get; set; }
        public string? enlaceImagen { get; set; }
    }
}
