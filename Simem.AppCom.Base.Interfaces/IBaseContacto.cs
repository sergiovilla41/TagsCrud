using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IBaseContacto
    {
        Task<ApiCrmCreateResponse> CreateContactRequest(ApiDocumentalDataRequestDto datosUsuario, string sessionId);
        Task<ApiDocumentalDataResponse> CreateContactRequestAPI(ApiDocumentalDataRequestDto dto);
        Task<ApiCrmCreateResponse> CreateRequestCrmAPI(ApiDocumentalDataResponse datosDocumental, ApiDocumentalDataRequestDto datosUsuario, string sessionId);
        Task SendCode(string userMail, string userName);
        Task<string> ValidateCode(string userMail, string code);
        List<ContactoEmpresa> GetCompanyList();
        List<ContactoPais> GetCountryList();
        List<ContactoSolicitante> GetApplicantList();
        List<ContactoTipoDocumento> GetDocumentTypesList();
        List<ContactoTipoSolicitud> GetRequestTypesList();
        ContactoConfiguracionAdjunto GetAttachmentConfig(string name);
    }
}
