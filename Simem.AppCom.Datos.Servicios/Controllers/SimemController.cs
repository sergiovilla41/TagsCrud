using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Simem.AppCom.Base.Utils;
using Simem.AppCom.Datos.Core;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using SImem.AppCom.Datos.Dto;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Simem.AppCom.Datos.Servicios.Controllers
{
    /// <summary>
    /// Api para la creaión y validación de tokens
    /// </summary>
    [Route("simem")]
    [ApiController]
    public class SimemController : ControllerBase
    {

        /// <summary>
        /// Se utiliza para validar el token de un usuario
        /// </summary>
        /// <param name="dataSet">Token del usuario</param>
        /// <returns></returns>
        /// <response code="200">El token ingresado es valido</response>
        /// <response code="401">El token ingresado no es valido</response>
        /// <response code="400">Ocurrio un error validando el token</response>
        [ExcludeFromCodeCoverage]
        [Route("validate-user")]
        [HttpPost]
        public async Task<IActionResult> HttpValidateUserAuth(string dataSet)
        {
            try
            {

                string authorization = "";

                if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
                {
                    authorization = authHeaderValue.ToString().Substring(7);
                }

                Guid _dataSet = Guid.Parse(dataSet);

                var email = await Task.FromResult(GraphManagerAuth.ValidateToken(authorization));

                if (!string.IsNullOrEmpty(email) && email != "expired")
                {
                    RolConfiguracionGeneracionArchivos rolConfig = new RolConfiguracionGeneracionArchivos();
                    rolConfig.CanReadDataSet(_dataSet, email);

                    return new OkObjectResult(new { state = "Ok", message = "true" });
                }
                else
                {
                    return new UnauthorizedResult();
                }

            }
            catch (Exception)
            {
                return  BadRequest( "Ocurrio un error validando el token");
            }
        }
    }
}
