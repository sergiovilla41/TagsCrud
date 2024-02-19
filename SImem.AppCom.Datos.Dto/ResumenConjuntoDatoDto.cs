using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SImem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ResumenConjuntoDatoDto
    {
        public Guid? IdConfiguracionGeneracionArchivos { get; set; }
        public string? IdDataset { get; set; }
        public string? NombreConjuntoDatos { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime InicioDato { get; set; }
        public DateTime? FinDato { get; set; }
        public DateTime? FechaDescarga { get; set; }
        public string? URLConexionAPI { get; set; }
        public string? URLConjuntoDatos { get; set; }
        public string? TipoPublicacion { get; set; }
    }
}
