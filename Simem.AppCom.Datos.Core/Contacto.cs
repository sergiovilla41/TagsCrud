using Simem.AppCom.Base.Interfaces;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dominio;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Simem.AppCom.Base.Utils;
using System.Diagnostics.CodeAnalysis;
using Azure;
using System.Diagnostics.Contracts;

namespace Simem.AppCom.Datos.Core
{
    [ExcludeFromCodeCoverage]
    public class Contacto : IBaseContacto
    {
        private readonly ContactoRepo _repo;
        public Contacto()
        {
            _repo ??= new ContactoRepo();
        }

        public async Task<ApiCrmCreateResponse> CreateContactRequest(ApiDocumentalDataRequestDto datosUsuario, string sessionId)
        {
            ApiCrmCreateResponse responseCRM;
            ApiDocumentalDataResponse responseGD = new();
            responseCRM = await CreateRequestCrmAPI(responseGD, datosUsuario, sessionId);
            return responseCRM;
        }
        
        public async Task<ApiDocumentalDataResponse> CreateContactRequestAPI(ApiDocumentalDataRequestDto dto)
        {
            ApiDocumentalDataResponse response = new ApiDocumentalDataResponse();
            if (dto.tipoSolicitud == TipoSolicitud.FallaTecnologica)
            {
                string? correo = KeyVaultManager.GetSecretValue(KeyVaultTypes.ContactanosFalla);
                await SendEmailFromAccountAsync(dto, correo);
            }
            else
            {
                response = await ConsumirApiCrm.getDataDocumentalAsync(dto);
            }

            return response;
        }

        public async Task<ApiCrmCreateResponse> CreateRequestCrmAPI(ApiDocumentalDataResponse datosDocumental, ApiDocumentalDataRequestDto datosUsuario, string sessionId)
        {
            DateTime fecha = DateTime.Now.ToUniversalTime().AddHours(-5.0);
            ApiCrmCreateResponse response = ConsumirApiCrm.setDataCrm(datosDocumental, fecha, datosUsuario, sessionId);

            if (response.case_number != null)
            {
                SeguridadHelper generateJWTToken = new();
                string token = generateJWTToken.CreateJWT(datosUsuario.radicador!, response.case_number);

                await SendEmailFromAccountAsync(datosUsuario, fecha, response.case_number, token);
            }
            if (response.id != null)
            {
                ContactoDto dto = new();
                dto.ConsecutivoCRM = response.case_number;
                dto.FechaInicio = fecha;
                await _repo.InsertContactRequest(dto);
            }

            return response;
        }

        public async Task SendCode(string userMail, string userName)
        {
            CorreoDto dto = new CorreoDto
            {
                CorreoUsuario = userMail,
                NombreUsuario = userName,
                CodigoVerificacion = CreateCode()
            };

            await SendCodeEmailAsync(dto);
            var response = await _repo.GetEmailRequest(dto.CorreoUsuario);
            if (response != null)
                await _repo.EditCodeRequest(response, dto.CodigoVerificacion);
            else
                await _repo.InsertCodeRequest(dto.CorreoUsuario, dto.CodigoVerificacion);
        }

        public async Task<string> ValidateCode(string userMail, string code)
        {
            string msg = "Fail";
            var response = await _repo.GetEmailRequest(userMail);
            if (response != null && response.Codigo == code)
            {
                DateTime now = DateTime.Now.ToUniversalTime().AddHours(-5.0);
                DateTime fecha = now.AddMinutes(-15);
                if (response.FechaCreacion >= fecha)
                {
                    msg = "Success";
                }
            }

            return msg;
        }

        static string CreateCode()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var chars = new char[6];

            for (int i = 0; i < chars.Length; i++)
            {
                var rand = RandomNumberGenerator.GetInt32(characters.Length);
                chars[i] = characters[rand];
            }

            return new string(chars);
        }

        static async Task SendCodeEmailAsync(CorreoDto dto)
        {
            string? apiKey = KeyVaultManager.GetSecretValue(KeyVaultTypes.ClBuzonPQR);
            SendGridClient? client = new SendGridClient(apiKey);
            string? mailbox = KeyVaultManager.GetSecretValue(KeyVaultTypes.BuzonPQR);
            EmailAddress? from = new EmailAddress(mailbox, "Contacto SiMEM");
            string? subject = "Código de verificación solicitud contacto SiMEM";
            EmailAddress? to = new EmailAddress(dto.CorreoUsuario, dto.NombreUsuario);
            string? plainTextContent = "";
            string? htmlContent = $"<p>Estimado(a) {dto.NombreUsuario},</p>" +
                "<p>Es un placer saludarte. Te informamos que hemos recibido una solicitud para acceder a la zona Contáctanos del SiMEM. Para garantizar la seguridad de tu cuenta, hemos generado un código de verificación que deberás utilizar para completar el proceso de acceso.</p>" +
                $"<p>Código de Verificación: {dto.CodigoVerificacion}</p>" +
                "<p>Este código es de uso único y tiene una validez de 15 minutos.</p>" +
                "<p>Por favor, ingresa el código en el campo código para completar la autenticación de tu correo</p>" +
                "<p>Si no realizaste esta solicitud puedes hacer caso omiso de este mensaje de correo electrónico. Otra persona puede haber escrito tu correo electrónico por error.</p>" +
                "<p>Gracias.</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }

        static async Task SendEmailFromAccountAsync(ApiDocumentalDataRequestDto dto, string correo)
        {
            string? apiKey = KeyVaultManager.GetSecretValue(KeyVaultTypes.ClBuzonPQR);
            SendGridClient? client = new SendGridClient(apiKey);
            string? mailbox = KeyVaultManager.GetSecretValue(KeyVaultTypes.BuzonPQR);
            EmailAddress? from = new EmailAddress(mailbox, "Contacto SiMEM");
            string? subject = "Falla tecnológica SIMEM";
            EmailAddress? to = new EmailAddress(correo, correo);
            string? plainTextContent = "";
            string? signature = EmailSignature();
            string? htmlContent = "<p>Buen día estimados, remitimos a ustedes el siguiente requerimiento</p><p></p>" +
                "<p>*************Solicitante**************</p>" +
                $"<p>Nombre: {dto.datosUsuario!.nombreCompleto}</p>" +
                $"<p>Correo: {dto.datosUsuario!.correo}</p>" +
                $"<p>Tipo Solicitud: {dto.tipoSolicitud}</p>" +
                $"<p>Empresa: {dto.empresa!.id} - {dto.empresa!.title}</p>" +
                $"<p>Teléfono: {dto.datosUsuario!.telefono}</p>" +
                $"<p>Tipo Documento Identidad: {dto.datosUsuario!.tipoDocumentoIdentidad}</p>" +
                $"<p>Número Documento Identidad: {dto.datosUsuario!.numeroDocumentoIdentidad}</p>" +
                $"<p>Cargo: {dto.datosUsuario!.cargo}</p>" +
                $"<p>Dirección: {dto.datosUsuario!.direccion}</p>" +
                $"<p>País: {dto.pais!.title}</p>" +
                "<p>********Descripción de la solicitud*******</p>" +
                $"<p>Asunto: {dto.asunto}</p>" +
                $"<p>Comentario/Solicitud: {dto.observaciones}</p>" +
                "<p>Agradecemos su ayuda gestionando la solicitud del usuario y enviando la respuesta al correo del solicitante.</p>" +
                "<p>Gracias.</p>" +
                $"<div>{signature}</div>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
            {
                new SendGrid.Helpers.Mail.Attachment
                {
                    Content = dto.archivo,
                    Filename = "adjunto.zip",
                    Type = "application/zip",
                    Disposition = "attachment"
                }
            };
            await client.SendEmailAsync(msg);
        }
        static async Task SendEmailFromAccountAsync(ApiDocumentalDataRequestDto dto, DateTime fecha, string caseNumber, string token)
        {
            string? url = KeyVaultManager.GetSecretValue(KeyVaultTypes.UrlContactanos);
            string? apiKey = KeyVaultManager.GetSecretValue(KeyVaultTypes.ClBuzonPQR);
            SendGridClient? client = new SendGridClient(apiKey);
            string? mailbox = KeyVaultManager.GetSecretValue(KeyVaultTypes.BuzonPQR);
            EmailAddress? from = new EmailAddress(mailbox, "Contacto SiMEM");
            string? subject = $"Confirmación de creación de la solicitud SiMEM {caseNumber}";
            EmailAddress? to = new EmailAddress(dto.datosUsuario!.correo, dto.datosUsuario!.nombreCompleto);
            string? plainTextContent = "";
            string? signature = EmailSignature();
            string? htmlContent = $"<p>Estimado (a) {dto.datosUsuario!.nombreCompleto},</p>" +
                $"<p>Hemos recibido exitosamente la {dto.tipoSolicitud} en nuestro sistema. Estamos trabajando activamente para atender tu solicitud de manera oportuna.</p>" +
                $"<p>Detalles de la solicitud:</p>" +
                "<p>----------------------------------------</p>" +
                $"<p>Fecha de creación: {fecha.ToString("yyyy-MM-dd HH:mm:ss")}</p>" +
                $"<p>Tipo Solicitud: {dto.tipoSolicitud}</p>" +
                $"<p>Estado: En proceso</p>" +
                $"<p>Asunto:</p>" +
                $"</p>{dto.asunto}</p>" +
                $"<p>Descripción:</p>" +
                $"</p>{dto.observaciones}</p>" +
                "<p>----------------------------------------</p>" +
                $"<p>Puedes mantenerte al tanto del progreso de tu solicitud <a href=\"{url}\">aquí</a>, autenticándote con el siguiente Token:</p>" +
                $"<p>Consecutivo: {caseNumber}</p>" +
                $"<p>Token: {token}</p>" +
                "<p>Gracias.</p>" +
                $"<div>{signature}</div>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }

        static string EmailSignature()
        {
            string? signature = $"<table Table border=0 cellspacing=0 cellpadding=0 style='border-collapse:collapse'>" +
                "<tr style='height:23.75pt'>" +
                "<td width=266 rowspan=4 style='width:199.3pt;padding:0cm 5.4pt 0cm 5.4pt;height:23.75pt'>" +
                "<p align=center style='text-align:center'><span><img border=0 width=251 height=45 style='width:2.618in;height:.4652in' src=\"https://stsimemprb.blob.core.windows.net/recursossimem/FirmaCorreo/img_logo.png\"></span></p></td>" +
                "<td width=16 style='width:11.8pt;padding:0cm 5.4pt 0cm 5.4pt;height:23.75pt'>" +
                "<p align=center style='text-align:center'><span>&nbsp;</span></p></td>" +
                "<td width=43 style='width:32.55pt;padding:0cm 5.4pt 0cm 5.4pt;height:23.75pt'>" +
                "<p align=center style='text-align:center'><span><img border=0 width=29 height=21 style='width:.3055in;height:.2222in' src=\"https://stsimemprb.blob.core.windows.net/recursossimem/FirmaCorreo/img_correo.png\"></span></p></td>" +
                "<td width=195 style='width:146.2pt;padding:0cm 5.4pt 0cm 5.4pt;height:23.75pt'>" +
                "<p><span style='font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;'><a href=\"mailto:info@xm.com.co\">info@xm.com.co</a></span></p></td></tr>" +
                "<tr style='height:23.75pt'>" +
                "<td width=1 rowspan=5 style='width:1;border-left:solid #A5A5A5 1.0pt'></td>" +
                "<td width=43 style='width:32.55pt;padding:0cm 5.4pt 0cm 5.4pt;height:23.75pt'><p align=center style='text-align:center'><span><img border=0 width=29 height=21 style='width:.3055in;height:.2222in' src=\"https://stsimemprb.blob.core.windows.net/recursossimem/FirmaCorreo/img_telefono.png\"></span></p></td>" +
                "<td width=195 style='width:146.2pt;padding:0cm 5.4pt 0cm 5.4pt;height:23.75pt'><p><span style='font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;'>(604)3172929 opción 1</span></p></td></tr>" +
                "<tr style='height:14.0pt'>" +
                "<td width=43 style='width:32.55pt;padding:0cm 5.4pt 0cm 5.4pt;height:14.0pt'><p align=center style='text-align:center'><span><img border=0 width=29 height=21 style='width:.3055in;height:.2222in' src=\"https://stsimemprb.blob.core.windows.net/recursossimem/FirmaCorreo/img_website.png\"></span></p></td>" +
                "<td width=195 style='width:146.2pt;padding:0cm 5.4pt 0cm 5.4pt;height:14.0pt'><p><u><span style='font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;'><a href=\"http://www.simem.co\">www.simem.co</a></span></u><span style='font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;'></span></p></td></tr>" +
                "<tr style='height:22.05pt'>" +
                "<td width=43 style='width:32.55pt;padding:0cm 5.4pt 0cm 5.4pt;height:22.05pt'><p align=center style='text-align:center'><span><img border=0 width=29 height=21 style='width:.3055in;height:.2222in' src=\"https://stsimemprb.blob.core.windows.net/recursossimem/FirmaCorreo/img_direccion.png\"></span></p></td>" +
                "<td width=195 style='width:146.2pt;padding:0cm 5.4pt 0cm 5.4pt;height:22.05pt'><p><span style='font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;'>Cll 12 Sur #18 </span><span style='font-family:\"Arial Rounded MT Bold\",sans-serif;color:black;'>&#8211;</span><span style='font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;'>168</span></p></td></tr>" +
                "<tr style='height:14.05pt'>" +
                "<td width=266 valign=top style='width:199.3pt;padding:0cm 5.4pt 0cm 5.4pt;height:14.05pt'><p align=center style='text-align:center'><span style='font-size:12.0pt;font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;letter-spacing:-.05pt;background:white;'>Sistema de Información para el </span><span style='font-size:12.0pt;font-family:\"Arial Rounded MT Bold\",sans-serif;'></span></p></td>" +
                "<td width=43 valign=top style='width:32.55pt;border:none;border-bottom:solid #A5A5A5 1.0pt;padding:0cm 5.4pt 0cm 5.4pt;height:14.05pt'><p align=center style='text-align:center'></p></td>" +
                "<td width=195 valign=top style='width:146.2pt;border:none;border-bottom:solid #A5A5A5 1.0pt;padding:0cm 5.4pt 0cm 5.4pt;height:14.05pt'><p><span style='font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;'>Medellín, Colombia</span></p></td></tr>" +
                "<tr style='height:10.0pt'>" +
                "<td width=266 valign=top style='width:199.3pt;padding:0cm 5.4pt 0cm 5.4pt;height:10.0pt'><p align=center style='text-align:center'><span style='font-size:12.0pt;font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;letter-spacing:-.05pt;background:white;'>Mercado de Energía Mayorista</span></p></td>" +
                "<td width=238 colspan=2 valign=bottom style='width:178.75pt;padding:0cm 5.4pt 0cm 5.4pt;height:10.0pt'><p align=center style='text-align:center'><span style='font-family:\"Arial Rounded MT Bold\",sans-serif;color:#767171;'>Administrado por XM</span></p></td></tr></table>";
            return signature;
        }

        public List<ContactoEmpresa> GetCompanyList()
        {
            return _repo.GetCompanyList();
        }

        public List<ContactoPais> GetCountryList()
        {
            return _repo.GetCountryList();
        }

        public List<ContactoSolicitante> GetApplicantList()
        {
            return _repo.GetApplicantList();
        }

        public List<ContactoTipoDocumento> GetDocumentTypesList()
        {
            return _repo.GetDocumentTypesList();
        }

        public List<ContactoTipoSolicitud> GetRequestTypesList()
        {
            return _repo.GetRequestTypesList();
        }

        public ContactoConfiguracionAdjunto GetAttachmentConfig(string name)
        {
            return _repo.GetAttachmentConfig(name);
        }
    }
}
