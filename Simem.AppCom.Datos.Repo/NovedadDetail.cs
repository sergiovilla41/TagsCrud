using Simem.AppCom.Datos.Dominio;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    [ExcludeFromCodeCoverage]
    public class NovedadDetail
    {
        public Novedad? Novedad { get; set; }
        public List<Etiqueta>? Etiquetas { get; set; }
    }
}
