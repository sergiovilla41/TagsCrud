using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApiDocumentalDataRequest
    {
        public string? asunto { get; set; }
        public TypeIdTitle? claseDocumental { get; set; }
        public TypeIdTitle? tipoDocumento { get; set; }
        public bool debeResponder { get; set; }
        public TypeIdTitle? empresa { get; set; }
        public TypeDependencia? dependencia { get; set; }
        public List<TypeMedioRecibido>? medioRecibo { get; set; }
        public string? fechaComunicado { get; set; }
        public string? numeroComunicado { get; set; }
        public string? observaciones { get; set; }
        public TypeIdTitle? pais { get; set; }
        public string? radicador { get; set; }
        public string? archivo { get; set; }
        public string? fileExtension { get; set; }
    }
}
