using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public interface IContactoRepo
    {
        Task InsertContactRequest(ContactoDto dto);
        Task DeleteContactRequest(Guid id);
        Task<ContactoCodigo> GetEmailRequest(string email);
        Task InsertCodeRequest(string email, string code);
        Task EditCodeRequest(ContactoCodigo entidad, string codigo);
        List<ContactoEmpresa> GetCompanyList();
        List<ContactoPais> GetCountryList();
        List<ContactoSolicitante> GetApplicantList();
        List<ContactoTipoDocumento> GetDocumentTypesList();
        List<ContactoTipoSolicitud> GetRequestTypesList();
        ContactoConfiguracionAdjunto GetAttachmentConfig(string name);
    }
}
