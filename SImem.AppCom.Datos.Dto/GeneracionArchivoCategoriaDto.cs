using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dominio;

namespace SImem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class GeneracionArchivoCategoriaDto
    {
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string? Titulo { get; set; }
        public string? TituloCategoriaNivel1 { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public List<Etiqueta>? Etiquetas { get; set; }
    }
}
