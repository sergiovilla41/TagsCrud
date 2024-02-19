using Azure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Base.Utils
{
    [ExcludeFromCodeCoverage]
    public static class ConsumirApiCrm
    {

        public static string getSessionApi()
        {

            var url = KeyVaultManager.GetSecretValue(KeyVaultTypes.urlCrmLogin);
            var user_name = KeyVaultManager.GetSecretValue(KeyVaultTypes.user_pqr);
            var password = KeyVaultManager.GetSecretValue(KeyVaultTypes.pass_pqr);
            var idSeccion = "";

            ApiCrmLoginParamRequest requestParams = new ApiCrmLoginParamRequest()
            {
                user_name =user_name,
                password = password,
                encryption = "PLAIN"
            };

            var requesrParamsJson = JsonConvert.SerializeObject(requestParams);
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("method", "login");
            request.AddParameter("input_type", "json");
            request.AddParameter("response_type", "json");
            request.AddParameter("rest_data", "[" + requesrParamsJson + "]");
            request.AddParameter("action", "API");
            RestResponse respuesta = client.Execute(request);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ApiCrmLoginResult respuestaApi = JsonConvert.DeserializeObject<ApiCrmLoginResult>(respuesta.Content!)!;
                idSeccion = respuestaApi!.id;
            }

            return idSeccion!;
        }

        public static ApiCrmDataResult getDataCrm(string consecutivo, string sessionid, string token)
        {
            var url = KeyVaultManager.GetSecretValue(KeyVaultTypes.urlCrmData);
            var crmModuleName = KeyVaultManager.GetSecretValue(KeyVaultTypes.crmModuleName);
            var crmStatus = KeyVaultManager.GetSecretValue(KeyVaultTypes.crmStatus);
            var crmWindow = KeyVaultManager.GetSecretValue(KeyVaultTypes.crmWindow);
            ApiCrmDataResult respuestaApi = new ApiCrmDataResult();
            DateTime primerDiaDelAño = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime ultimoDiaDelAño = new DateTime(DateTime.Now.Year, 12, 31);
            ApiCrmParamRequest paramRequest = new ApiCrmParamRequest()
            {
                session = sessionid,
                module_name = crmModuleName,
                start = primerDiaDelAño.ToString("yyyy-MM-dd"),
                end = ultimoDiaDelAño.ToString("yyyy-MM-dd"),
                recept_start = "",
                recept_end = "",
                status = crmStatus,
                type = "",
                window = crmWindow,
                case_number = consecutivo,
                response = "",
                reception = "",
                response_start = "",
                response_end = "",
                modified_start = "",
                modified_end = "",
                limit = "20",
                offset = "0"
            };
            var paramsJson = JsonConvert.SerializeObject(paramRequest);

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("method", "get_full_cases_report");
            request.AddParameter("input_type", "json");
            request.AddParameter("response_type", "json");
            request.AddParameter("rest_data", paramsJson);
            RestResponse respuesta = client.Execute(request);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                respuestaApi = JsonConvert.DeserializeObject<ApiCrmDataResult>(respuesta.Content!)!;
            }
            return respuestaApi;
        }

        public static async Task<ApiDocumentalDataResponse> getDataDocumentalAsync(ApiDocumentalDataRequestDto objectJson)
        {
            ApiDocumentalDataResponse response = new ApiDocumentalDataResponse();
            string? url = KeyVaultManager.GetSecretValue(KeyVaultTypes.DocumentalUrl);
            string? user = KeyVaultManager.GetSecretValue(KeyVaultTypes.DocumentalUser);
            string? pass = KeyVaultManager.GetSecretValue(KeyVaultTypes.DocumentalPass);
            string? arrayMedio = KeyVaultManager.GetSecretValue(KeyVaultTypes.DocumentalMedioRecibido);
            string? claseDocumental = KeyVaultManager.GetSecretValue(KeyVaultTypes.DocumentalClaseDocumental);
            string? dependencia = KeyVaultManager.GetSecretValue(KeyVaultTypes.DocumentalDependencia);
            bool blnResponder = bool.Parse(KeyVaultManager.GetSecretValue(KeyVaultTypes.DocumentalDebeResponder));
            TypeMedioRecibido medioRecibido = new TypeMedioRecibido()
            {
                id = int.Parse(arrayMedio.Split(';')[0]),
                title = arrayMedio.Split(';')[1],
                numeroVia = arrayMedio.Split(';')[2]
            };
            objectJson.medioRecibo = new List<TypeMedioRecibido>();
            objectJson.medioRecibo.Add(medioRecibido);

            string? subject = string.Empty;
            if (objectJson.empresa!.title == "PERSONA NATURAL")
            {
                subject = $"{objectJson.datosUsuario!.apellido} {objectJson.datosUsuario!.nombre}.{objectJson.observaciones!}";
            }
            else
            {
                subject = objectJson.observaciones;
            }

            ApiDocumentalDataRequest paramRequest = new ApiDocumentalDataRequest()
            {
                asunto = subject!.ToUpper(),
                claseDocumental = new TypeIdTitle
                {
                    id = int.Parse(claseDocumental.Split(';')[0]),
                    title = claseDocumental.Split(';')[1]
                },
                tipoDocumento = objectJson.tipoDocumento,
                debeResponder = blnResponder,
                empresa = objectJson.empresa,
                dependencia = new TypeDependencia
                {
                    id = int.Parse(dependencia.Split(";")[0]),
                    title = dependencia.Split(";")[1],
                    codigoDependencia = dependencia.Split(";")[2]
                },
                medioRecibo = objectJson.medioRecibo,
                fechaComunicado = DateTime.Now.ToUniversalTime().AddHours(-5.0).ToString("yyyy-MM-ddTHH:mm:ss"),
                numeroComunicado = "N/A",
                observaciones = "Informacion recibida por el aplicativo SIMEM",
                pais = objectJson.pais,
                radicador = objectJson.radicador,
                archivo = objectJson.archivo,
                fileExtension = objectJson.fileExtension
            };
            var paramsJson = JsonConvert.SerializeObject(paramRequest);

            var uri = new Uri(url);
            var credentialsCache = new CredentialCache();
            credentialsCache.Add(uri, "NTLM", new NetworkCredential(user, pass));
            var handler = new HttpClientHandler() { Credentials = credentialsCache };
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var httpClient = new HttpClient(handler) { Timeout = new TimeSpan(0, 3, 10) };
            var respuesta = await httpClient.PostAsync(uri, new StringContent(paramsJson, Encoding.UTF8, "application/json"));

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var result = await respuesta.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ApiDocumentalDataResponse>(result)!;
            }

            return response;
        }

        public static ApiCrmCreateResponse setDataCrm(ApiDocumentalDataResponse datosDocumental, DateTime fecha, ApiDocumentalDataRequestDto datosUsuario, string sessionId)
        {
            string? url = KeyVaultManager.GetSecretValue(KeyVaultTypes.urlCrmData);
            string? type = datosUsuario.tipoSolicitud == TipoSolicitud.Oportunidades ? "SUGERENCIA" : "COM_TRAMITE";
            string? newDate = string.Empty;
            string? siadRef = string.Empty;
            if (datosUsuario.tipoSolicitud == TipoSolicitud.Oportunidades || datosDocumental.records == null)
            {
                newDate = fecha.ToString("yyyy-MM-dd HH:mm:ss");
                siadRef = "";
            }
            else
            {
                if (datosDocumental.records != null)
                {
                    var date = DateTime.Parse(datosDocumental.records!.fechaRadicacion!);
                    newDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    siadRef = datosDocumental.records!.numeroRadicado!;
                }
            }
            ApiCrmCreateResponse respuestaApi = new ApiCrmCreateResponse();
            ApiCrmCreateRequest paramRequest = new ApiCrmCreateRequest()
            {
                session = sessionId,
                name_value_list = new ApiValueList()
                {
                    type = type,
                    aig_reception_type = "S", //Indica que es API CRM
                    aig_reception_date = newDate,
                    work_team = KeyVaultManager.GetSecretValue(KeyVaultTypes.crmWorkTeam),
                    priority = KeyVaultManager.GetSecretValue(KeyVaultTypes.crmPriority),
                    name = datosUsuario.asunto,
                    aig_siad_ref = siadRef,
                    description = datosUsuario.observaciones,
                    aig_contact_name = datosUsuario.radicador,
                    aig_account_name = datosUsuario.empresa!.title,
                    aig_contact_phone_work = datosUsuario.datosUsuario!.telefono,
                    aig_contact_email = datosUsuario.datosUsuario!.correo,
                    aig_contact_id_number = $"{datosUsuario.datosUsuario.tipoDocumentoIdentidad} {datosUsuario.datosUsuario.numeroDocumentoIdentidad}",
                    attachment = datosUsuario.archivo,
                    aig_contact_title = string.Empty,
                    aig_contact_phone_other = string.Empty,
                    aig_contact_phone_mobile = string.Empty,
                    aig_contact_phone_mobile2 = string.Empty
                }
            };
            var paramsJson = JsonConvert.SerializeObject(paramRequest);

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("method", "set_xm_case");
            request.AddParameter("input_type", "json");
            request.AddParameter("response_type", "json");
            request.AddParameter("rest_data", paramsJson);
            RestResponse respuesta = client.Execute(request);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                respuestaApi = JsonConvert.DeserializeObject<ApiCrmCreateResponse>(respuesta.Content!)!;
            }
            return respuestaApi;
        }

        public static ApiCrmAttachmentResponse getAttachmentCrm(string sessionId, string attachmentId)
        {
            string? url = KeyVaultManager.GetSecretValue(KeyVaultTypes.urlCrmLogin);
            ApiCrmAttachmentResponse respuestaApi = new ApiCrmAttachmentResponse();
            ApiCrmAttachmentRequest paramRequest = new ApiCrmAttachmentRequest()
            {
                session = sessionId,
                id = attachmentId
            };
            var paramsJson = JsonConvert.SerializeObject(paramRequest);

            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("method", "get_note_attachment");
            request.AddParameter("input_type", "json");
            request.AddParameter("response_type", "json");
            request.AddParameter("rest_data", paramsJson);
            RestResponse respuesta = client.Execute(request);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                respuestaApi = JsonConvert.DeserializeObject<ApiCrmAttachmentResponse>(respuesta.Content!)!;
            }
            return respuestaApi;
        }

    }
}
