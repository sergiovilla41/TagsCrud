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
    /// Controlador para obtener los valores de la configuración de las columnas de los dataset.
    /// </summary>
    [Route("dataset-column")]
    public class DataSetColumnController : Controller
    {
        /// <summary>
        /// Obtiene la configuración de las columnas de un conjunto de datos.
        /// </summary>
        /// <param name="idData">id del conjunto de datos.</param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">Consulta generada sin resultados</response>
        /// <response code="400">Error al generar la solicitud</response>
        [HttpGet]
        public async Task<IActionResult> HttpGetDataSetColumn([BindRequired] string idData)
        {
            try
            {
                Guid dataId = new Guid(idData);
                DataSetColumn dataSetColumnCore = new DataSetColumn();
                var result = await dataSetColumnCore.GetDataSetColumns(dataId);

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
        /// Obtiene la configuración de las columnas del resumen de conjunto de datos.
        /// </summary>
        /// <param name="Id">id del conjunto de datos.</param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">Consulta generada sin resultados</response>
        /// <response code="400">Error al generar la solicitud</response>
        [Route("dataset-column-variable")]
        [HttpGet]
        public async Task<IActionResult> HttpGetDataSetColumnVariable([BindRequired] string Id)
        {
            try
            {
                DataSetColumn dataSetColumnCore = new DataSetColumn();
                var result = await dataSetColumnCore.GetDataSetColumnsVariable(Id);

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
        /// Obtiene listado estandarizado de las columnas de un conjunto de datos.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">Consulta generada sin resultados</response>
        /// <response code="400">Error al generar la solicitud</response>
        [Route("standardization-register")]
        [HttpGet]
        public async Task<IActionResult> HttpGetEstandarizacionRegistro(string dataId)
        {
            try
            {
                DataSetColumn dataSetColumnCore = new DataSetColumn();
                var result = await dataSetColumnCore.GetEstandarizacionRegistro(Guid.Parse(dataId));

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
                return BadRequest(new { messageError = $"Ocurrió un error: " + ex.Message });
            }
        }
    }
}
