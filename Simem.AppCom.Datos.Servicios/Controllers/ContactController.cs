using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Simem.AppCom.Base.Utils;
using Simem.AppCom.Datos.Core;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using System.Text.Json;

namespace Simem.AppCom.Datos.Servicios.Controllers
{
    /// <summary>
    /// Controlador para la gestión de contacto de SIMEM.
    /// </summary>
    [Route("contact")]
    public class ContactController : ControllerBase
    {
        /// <summary>
        /// Genera codígo de validación al correo para continuar con el formulario de contacto. 
        /// </summary>
        /// <param name="exception">Genera una exepción validar control de exepciones</param>
        /// <param name="email">Correo del usuario que genera la solicitud</param>
        /// <param name="userName">Nombre del usuario que realiza la solicitud</param>
        /// <returns></returns>
        /// <response code="200">Generación de código exitoso</response>
        /// <response code="400">Error al generar el código</response>
        [Route("code")]
        [HttpGet]
        public async Task<IActionResult> HttpGetContactoCodigo(string? exception, [BindRequired] string email, [BindRequired] string userName)
        {
            try
            {
                _ = Convert.ToBoolean(exception);

                string correo = email;
                string nombre = userName;

                Contacto contacto = new();
                await Task.Run(() => contacto.SendCode(correo, nombre));

                return Ok(new { message = "Código enviado" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = $"Ocurrió un error: " + ex.StackTrace });
            }
        }

        /// <summary>
        /// Consulta el estado de una solicitud
        /// </summary>
        /// <param name="consecutive">Consecutivo de la solicitud</param>
        /// <param name="token">Token de autorización para realizar la consulta.</param>
        /// <returns></returns>
        /// <response code="200">Consulta estado de la solicitud exitoso</response>
        /// <response code="400">Error al generar la consulta</response>
        [Route("data-crm")]
        [HttpGet]
        public IActionResult HttpGetContactoDatosCrm([BindRequired] string consecutive, [BindRequired] string token)
        {
            ApiCrmDataResponse response = new ApiCrmDataResponse();
            response.status = "false";
            response.message = "Ha ocurrido una dificultad técnica, porfavor intentalo de nuevo";
            try
            {
                var seccionId = ConsumirApiCrm.getSessionApi();
                if (seccionId == "" || seccionId == null)
                {
                    return  BadRequest( response);
                }

                JwtDataDto jwtDataDto = SeguridadHelper.ReadJWT(token);

                if (jwtDataDto.user == "")
                {
                    response.status = "false";
                    response.message = "Token inválido";
                    return  BadRequest( response);
                }

                ApiCrmDataResult result = ConsumirApiCrm.getDataCrm(consecutive, seccionId, token);
                if (result != null)
                {
                    if (result.cases_list?.Length > 0)
                    {
                        if (result.cases_list[0].contacto != jwtDataDto.user || result.cases_list[0].case_number != jwtDataDto.code)
                        {
                            response.status = "false";
                            response.message = "Token inválido";
                            return  BadRequest( response);
                        }

                        response.status = "true";
                        response.message = "";
                        response.data = result.cases_list;
                        return Ok(response);
                    }
                    else
                    {
                        response.status = "false";
                        response.message = "El consecutivo no existe o la petición no fue creada a través de la plataforma SiMEM";
                        return  BadRequest( response);
                    }
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return  BadRequest( response);
            }
        }

        /// <summary>
        /// Obtiene la lista de empresas que pueden realizar la solicitud.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">consulta generada sin resultados</response>
        /// <response code="400">Ha ocurrido un error en la consulta</response>
        [Route("company-list")]
        [HttpGet]
        public async Task<IActionResult> HttpGetContactoListadoEmpresas()
        {
            try
            {
                Contacto core = new();
                var response = await Task.Run(() => core.GetCompanyList());
                if (response.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = ex.Message });
            }            
        }

        /// <summary>
        /// Obtiene el listado de paises para seleccionar de donde proviene la solicitud.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">consulta generada sin resultados</response>
        /// <response code="400">Ha ocurrido un error en la consulta</response>
        [Route("country-list")]
        [HttpGet]
        public async Task<IActionResult> HttpGetContactoListadoPaises()
        {
            try
            {
                Contacto core = new();
                var response = await Task.Run(() => core.GetCountryList());
                if (response.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = ex.Message });
            }
            
        }

        /// <summary>
        /// Listado de tipo de persona.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">consulta generada sin resultados</response>
        /// <response code="400">Ha ocurrido un error en la consulta</response>
        [Route("request-list")]
        [HttpGet]
        public async Task<IActionResult> HttpGetContactoListadoSolicitantes()
        {
            try
            {
                Contacto core = new();
                var response = await Task.Run(() => core.GetApplicantList());
                if (response.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene listado de tipo de documento
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">consulta generada sin resultados</response>
        /// <response code="400">Ha ocurrido un error en la consulta</response>
        [Route("type-document-list")]
        [HttpGet]
        public async Task<IActionResult> HttpGetContactoListadoTiposDocumento()
        {
            try
            {
                Contacto core = new();
                var response = await Task.Run(() => core.GetDocumentTypesList());
                if (response.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene listado de tipo de solicitud.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">consulta generada sin resultados</response>
        /// <response code="400">Ha ocurrido un error en la consulta</response>
        [Route("type-request-list")]
        [HttpGet]
        public async Task<IActionResult> HttpGetContactoListadoTiposSolicitud()
        {
            try
            {
                Contacto core = new();
                var response = await Task.Run(() => core.GetRequestTypesList());
                if (response.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = ex.Message });
            }
        }

        /// <summary>
        /// Valida el codigo generado por la solicitud.
        /// </summary>
        /// <param name="email">Correo del usuario</param>
        /// <param name="code">Codigo de validación.</param>
        /// <returns></returns>
        /// <response code="200">Validacón de código generada con éxito</response>
        /// <response code="204">Validación generada sin resultados</response>
        /// <response code="400">Ha ocurrido un error en la consulta</response>
        [Route("validate-code")]
        [HttpGet]
        public async Task<IActionResult> HttpGetContactoValidarCodigo([BindRequired]  string email, [BindRequired] string code)
        {
            try
            {
                Contacto core = new();
                var response = await Task.Run(() => core.ValidateCode(email, code));

                if (!string.IsNullOrEmpty(response))
                {
                    return Ok(new { message = response });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = ex.Message });
            }
            
        }

        /// <summary>
        /// Crea Solicitud de contacto. 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Solicitud creada con éxito</response>
        /// <response code="204">Validación generada sin resultados</response>
        /// <response code="400">Error al crear la solicitud</response>
        [HttpPost]
        public async Task<IActionResult> HttpPostContacto([FromBody, BindRequired] ApiDocumentalDataRequestDto entity)
        {
            try
            {
                if (entity == null)
                {
                    return NoContent();
                }

                Contacto contacto = new();
                var sessionId = ConsumirApiCrm.getSessionApi();
                if (sessionId == "" || sessionId == null)
                {
                    return  BadRequest( new { message = "Error con el login a la API de CRM" });
                }

                var response = await contacto.CreateContactRequest(entity, sessionId);
                if (response.id != null)
                {
                    return Ok(new { message = true });
                }
                else
                {
                    return  BadRequest( new { message = "Error al consumir API" });
                }
            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = "Error al consumir API: " + ex.Message });
            }
        }

        /// <summary>
        /// Obtiene los adjuntos de una solicitud.
        /// </summary>
        /// <param name="attachmentId">Id del adjunto</param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="400">Error al generar la solicitud</response>
        [Route("attachment-crm")]
        [HttpGet]
        public async Task<IActionResult> HttpGetContactoAttachmentCrm([BindRequired]  string attachmentId)
        {
            try
            {
                var sessionId = ConsumirApiCrm.getSessionApi();
                if (sessionId == "" || sessionId == null)
                {
                    return  BadRequest( new { message = "Error con el login a la API de CRM" });
                }

                ApiCrmAttachmentResponse respuesta = await Task.FromResult(ConsumirApiCrm.getAttachmentCrm(sessionId, attachmentId));

                return Ok(respuesta.note_attachment);

            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = "Error al consumir API: " + ex.Message });
            }
        }

        /// <summary>
        /// Obtiene el valor de la configuración de adjuntos en una solicitud.
        /// </summary>
        /// <param name="configName">Nombre de la configuración.</param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="400">Ha ocurrido un error en la consulta</response>
        [Route("attachment-config")]
        [HttpGet]
        public async Task<IActionResult> HttpGetContactoConfiguracionAdjunto([BindRequired]  string configName)
        {
            try
            {
                string nombre = configName;
                Contacto core = new();
                var response = await Task.Run(() => core.GetAttachmentConfig(nombre));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = ex.Message });
            }
        }
    }
}
