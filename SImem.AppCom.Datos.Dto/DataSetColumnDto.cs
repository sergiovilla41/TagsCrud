using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class DataSetColumnDto
    {
        public Guid IdColumnaDestino { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? NombreColumnaOrigen { get; set; }
        public int NumeracionColumna { get; set; }
        public bool EsFiltro { get; set; }
        public string? TipoFiltro { get; set; }
        public string? TipoDato { get; set; }
        public string? ValorObjetivo { get; set; }
    }
}
