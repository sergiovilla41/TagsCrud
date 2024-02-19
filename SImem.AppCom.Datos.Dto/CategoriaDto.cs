using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    public class CategoriaDto
    {
        public Guid? Id { get; set; }
        public string? Titulo { get; set; }
        public string? Icono { get; set; }
        public bool Estado { get; set; }
        public string? Descripcion { get; set; }
        public int? ConjuntoDato { get; set; }
        public string? UltimaActualizacion { get; set; }
        public string? UltimaActualizacionDatoTitulo { get; set; }
        public Guid? UltimaActualizacionDatoId { get; set; }
        public string? Descarga { get; set; }
        public int OrdenCategoria { get; set; }
        public bool privado { get; set; }
        public int CantidadConjuntoDato { get; set; }
        public string? Nivel { get; set; }
    }
}
