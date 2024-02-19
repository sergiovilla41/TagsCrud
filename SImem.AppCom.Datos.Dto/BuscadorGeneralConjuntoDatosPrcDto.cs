using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SImem.AppCom.Datos.Dto
{
    public class BuscadorGeneralConjuntoDatosPrcDto
    {
        public int totalFilas { get; set; }
        public string? etiquetas { get; set; }
        public string? variables { get; set; }
        public string? resultadoJson { get; set; }
    }
}

