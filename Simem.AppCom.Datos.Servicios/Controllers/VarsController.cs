using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    /// Api dedicada a la manipulación del inventario de variables de SIMEM
    /// </summary>
    [Route("vars")]
    public class VarsController : ControllerBase
    {
        /// <summary>
        /// Devuelve todas las variables que incluyan el id ingresado del conjunto de datos
        /// </summary>
        /// <param name="idData">Id conjunto de datos</param>
        /// <returns></returns>
        /// <response code="200">Lista con las variables dle inventario de variables</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">Ocurrió un error obteniendo las variables del inventario de variables</response>
        [Route("list-by-data-id/{idData}")]
        [HttpGet]
        public async Task<IActionResult> HttpGetDataConfiguracionVariable(string idData)
        {
            try
            {
                Guid dataId = new Guid(idData);
                ConfiguracionVariable configuracionVariable = new ConfiguracionVariable();
                var result = await configuracionVariable.GetDataById(dataId);
                if (result.Count > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
               return  BadRequest( new { message = ex.Message });
            }
        }


        /// <summary>
        /// Devuelve la variable que coincida con el Id ingresado
        /// </summary>
        /// <param name="Id">Id de la variable</param>
        /// <returns></returns>
        /// <response code="200">Variable correspondiente al id ingresado</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">No se puede procesar la solicitud</response>
        [HttpGet("{Id}")]
        public async Task<IActionResult> HttpGetVariableById(string Id)
        {
            try
            {
                Core.Variable VarRepo = await Task.Run(() => new Core.Variable());
                var Variable = VarRepo.GetVariableById(Guid.Parse(Id));

                if (Variable.Count > 0)
                {
                    return Ok(new { message = "Ok", datos = Variable });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }

        /// <summary>
        /// Realiza un filtro de las variables a partir del texto ingresado en el parametro filtro
        /// </summary>
        /// <param name="filtro">Texto para filtrar</param>
        /// <returns></returns>
        /// <response code="200">Variable correspondiente al id ingresado</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">No se puede procesar la solicitud</response>
        [Route("filtered-by-title/{filtro?}")]
        [HttpGet]
        public async Task<IActionResult> HttpGetVariableFilteredByTitle(string? filtro)
        {
            try
            {
                filtro ??= "";
                Core.Variable VarRepo = new Core.Variable();
                var ListaVariables = await Task.Run(() => VarRepo.GetVariablesFilteredByTitle(filtro));

                if (ListaVariables.result.Count > 0)
                {
                    return Ok(new { message = "Ok", datos = ListaVariables });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
           
        }

        /// <summary>
        /// Devuelve todas las variables en el inventario de variables
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Lista con todas las variables</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">No se puede procesar la solicitud</response>
        [HttpGet]
        public async Task<IActionResult> HttpGetVariableInventory()
        {
            try
            {
                Variable VarRepo = new();
                var ListaVariables = await Task.Run(() => VarRepo.GetVariables());

                if (ListaVariables != null)
                {
                    return Ok(new { message = "Ok", datos = ListaVariables });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
    }
}
