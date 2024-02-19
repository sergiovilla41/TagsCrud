using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApiDocumentalDataRequestDto
    {
        public UserData? datosUsuario { get; set; }
        public string? asunto { get; set; }
        public TypeIdTitle? claseDocumental { get; set; }
        public string? tipoSolicitud { get; set; }
        public TypeIdTitle? tipoDocumento { get; set; }
        public bool debeResponder { get; set; }
        public TypeIdTitle? empresa { get; set; }
        public TypeDependencia? dependencia { get; set; }
        public List<TypeMedioRecibido>? medioRecibo { get; set; }
        public string? fechaComunicado { get; set; }
        public string? numeroComunicado { get; set; }
        public string? contrato { get; set; }
        public string? observaciones { get; set; }
        public TypeIdTitle? pais { get; set; }
        public string? procesoCompra { get; set; }
        public string? numeroQueja { get; set; }
        public string? proyecto { get; set; }
        public string? radicador { get; set; }
        public string? archivo { get; set; }
        public string? fileExtension { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class UserData
    {
        public string? nombreCompleto { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? correo { get; set; }
        public string? telefono { get; set; }
        public string? tipoDocumentoIdentidad { get; set; }
        public string? numeroDocumentoIdentidad { get; set; }
        public string? cargo { get; set; }
        public string? direccion { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TypeIdTitle
    {
        public int id { get; set; }
        public string? title { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TypeDependencia
    {
       public int id { get; set; }
        public string? title { get; set; }
        public string? codigoDependencia { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TypeMedioRecibido
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? numeroVia { get; set; }
    }
}
