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
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Simem.AppCom.Datos.Servicios.Controllers
{
    /// <summary>
    /// Controlador para consultar los conjuntos de datos. 
    /// </summary>
    [Route("file-generation")]
    public class FileGenerationController : ControllerBase
    {
        /// <summary>
        /// Obtiene la información correspondiente para ser mostrada en la ventana de detalle resumen conjunto dato.
        /// </summary>
        /// <param name="id">Id del dato de resumen conjunto dato</param>
        /// <param name="pageIndex">Indice</param>
        /// <param name="pageSize">Tamaño pagina</param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">No se encontraron resultados</response>
        /// <response code="400">Error al generar la consulta</response>
        [HttpGet("catalog-variable-dataset")]
        public async Task<IActionResult> GetCatalogVariableDataset(string id, int? pageIndex, int? pageSize)
        {
            try
            {
                Dato core = new();
                Paginador? paginador = null;
                if (pageIndex != null && pageSize != null)
                {
                    paginador = new() { PageIndex = (int)pageIndex, PageSize = (int)pageSize };
                }

                var entity = await core.GetDataVariable(id);
                if (entity.Titulo == ResumenConjuntoTipoVista.Inventario)
                {
                    var result = await core.GetAllVariables(paginador, null, null);
                    if (result.Count > 0)
                    {
                        return Ok(new { status = true, data = result });
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    var result = await core.GetAllDataSets(paginador, null, null);
                    if (result.Count > 0)
                    {
                        return Ok(new { status = true, data = result });
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = ex.Message });
            }
        }
        /// <summary>
        /// Obtiene la información filtrada correspondiente para ser mostrada en la ventana de detalle resumen conjunto dato.
        /// </summary>
        /// <param name="id">Id del dato de resumen conjunto dato</param>
        /// <param name="fechaInicio">Fecha inicial</param>
        /// <param name="fechaFin">Fecha final</param>
        /// <param name="pageIndex">Indice</param>
        /// <param name="pageSize">Tamaño pagina</param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con éxito</response>
        /// <response code="204">No se encontraron resultados</response>
        /// <response code="400">Error al generar la consulta</response>
        [HttpGet("filter")]
        public async Task<IActionResult> Filter(string id, DateTime fechaInicio, DateTime fechaFin, int? pageIndex, int? pageSize)
        {
            try
            {
                Dato core = new();
                Paginador? paginador = null;
                if (pageIndex != null && pageSize != null)
                {
                    paginador = new() { PageIndex = (int)pageIndex, PageSize = (int)pageSize };
                }

                var entity = await core.GetDataVariable(id);
                if (entity.Titulo == ResumenConjuntoTipoVista.Inventario)
                {
                    var result = await core.GetAllVariables(paginador, fechaInicio, fechaFin);
                    if (result.Count > 0)
                    {
                        return Ok(new { status = true, data = result });
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    var result = await core.GetAllDataSets(paginador, fechaInicio, fechaFin);
                    if (result.Count > 0)
                    {
                        return Ok(new { status = true, data = result });
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = ex.Message });
            }
        }
        /// <summary>
        /// Suma la cantidad de descargas de un conjunto de datos.
        /// </summary>
        /// <param name="request.Id">Id de generación archivo</param>
        /// <returns></returns>
        /// <response code="200">Actualización de descargas exitoso</response>
        /// <response code="400">Error al actualizar las descargas</response>
        [Route("add-download-count")]
        [HttpPost]
        public async Task<IActionResult> AddDownload([BindRequired, FromBody] IdRequest request)
        {
            if (request == null)
                return  BadRequest( new { messageError = "Debe enviar el IdConfiguracionGeneracionArchivos" });

            try
            {
                Dato dato = new();
                await Task.Run(() => dato.AddDownload(Guid.Parse(request.Id!)));
                return Ok(new { message = "Cantidad de descargas actualizada" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = ex.Message });
            }
        }
        /// <summary>
        /// Suma la cantidad de descargas del resumen de conjunto de datos.
        /// </summary>
        /// <param name="request.Id">Id del registro de resumen de conjunto de datos</param>
        /// <returns></returns>
        /// <response code="200">Actualización de descargas exitoso</response>
        /// <response code="400">Error al actualizar las descargas</response>
        [Route("add-download-variable-count")]
        [HttpPost]
        public async Task<IActionResult> AddDownloadVariable([BindRequired, FromBody] IdRequest request)
        {
            if (request == null)
                return  BadRequest( new { messageError = "Debe enviar el Id" });

            try
            {
                Dato dato = new();
                await Task.Run(() => dato.AddDownloadVariable(request.Id!));
                return Ok(new { message = "Cantidad de descargas actualizada" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = ex.Message });
            }
        }

        /// <summary>
        /// Suma cantidad de visitas de un conjunto de datos.
        /// </summary>
        /// <param name="request.Id">id de configuración generación archivos.</param>
        /// <returns></returns>
        /// <response code="200">Actualización de visitas exitoso</response>
        /// <response code="400">Error al actualizar las visitas</response>
        [Route("add-view-count")]
        [HttpPost]
        public async Task<IActionResult> AddViewCount([BindRequired][FromBody] IdRequest request)
        {
            if (request == null)
            {
                return  BadRequest( new { messageError = "Debe enviar el IdConfiguracionGeneracionArchivos" });
            }

            Dato dato = new Dato();
            try
            {
                await Task.Run(() => dato.AddView(Guid.Parse(request.Id!)));
                return Ok(new { message = "Cantidad de vistas actualizada" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = ex.Message });
            }
        }

        /// <summary>
        /// Suma cantidad de visitas del resumen de conjunto de datos.
        /// </summary>
        /// <param name="request.Id">Id del registro de resumen de conjunto de dato.</param>
        /// <returns></returns>
        /// <response code="200">Actualización de visitas exitoso</response>
        /// <response code="400">Error al actualizar las visitas</response>
        [Route("add-view-variable-count")]
        [HttpPost]
        public async Task<IActionResult> AddViewVariableCount([BindRequired][FromBody] IdRequest request)
        {
            if (request == null)
            {
                return  BadRequest( new { messageError = "Debe enviar el Id" });
            }

            Dato dato = new Dato();
            try
            {
                await Task.Run(() => dato.AddViewVariable(request.Id!));
                return Ok(new { message = "Cantidad de vistas actualizada" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la información de un conjunto de datos.
        /// </summary>
        /// <param name="id">id generación archivo</param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con  éxito</response>
        /// <response code="401">No autorizado para realizar la consulta</response>
        /// <response code="400">Error al generar la consulta</response>
        [ExcludeFromCodeCoverage]
        [HttpGet]
        public async Task<IActionResult> HttpGetData([BindRequired] string id)
        {
            try
            {
                Dato core = new Core.Dato();
                bool isPrivate = core.isPrivateDataSet(Guid.Parse(id));
                if (isPrivate)
                {
                    string authorization = "";

                    if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
                    {
                        authorization = authHeaderValue.ToString().Substring(7);
                    }

                    if (!string.IsNullOrEmpty(authorization))
                    {
                        string email = GraphManagerAuth.ValidateToken(authorization);
                        RolConfiguracionGeneracionArchivos rolConfig = new RolConfiguracionGeneracionArchivos();
                        if (!rolConfig.CanReadDataSet(Guid.Parse(id), email))
                        {
                            return Unauthorized();
                        }
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }

                var entity = await Task.Run(() => core.GetData(Guid.Parse(id)));
                return Ok(new { status = true, message = entity, isPrivate });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
        /// <summary>
        /// Obtiene la información de un conjunto de datos.
        /// </summary>
        /// <param name="id">id generación archivo</param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con  éxito</response>
        /// <response code="204">No se encuentran datos en la consulta</response>
        /// <response code="400">Error al generar la consulta</response>
        [Route("get-data-variable")]
        [ExcludeFromCodeCoverage]
        [HttpGet]
        public async Task<IActionResult> HttpGetDataVariable([BindRequired] string id)
        {
            try
            {
                Dato core = new Core.Dato();
                DatoDto entity = await Task.Run(() => core.GetDataVariable(id));
                if (entity == null)
                    return NoContent();
                return Ok(new { status = true, message = entity });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = ex.Message });
            }
        }
        /// <summary>
        /// Obtiene los ultimos conjunto de datos actualizados.
        /// </summary>
        /// <param name="exception">Genera una exepción</param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con  éxito</response>
        /// <response code="204">Consulta generada sin resultados</response>
        /// <response code="500">Error al generar la consulta</response>
        [Route("last-updated-data")]
        [HttpGet]
        public async Task<IActionResult> HttpGetLastUpdatedDatas(string? exception)
        {

            try
            {
                _ = Convert.ToBoolean(exception);
                Dato dataCore = new Dato();
                var datas = await dataCore.GetLastUpdatedData();


                if (datas.Count > 0)
                {
                    return Ok(new { message = datas });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="typeViewId"></param>
        /// <param name="tagsId"></param>
        /// <param name="ordenar"></param>
        /// <param name="texto"></param>
        /// <returns></returns>
        /// <response code="200">Consulta generada con  éxito</response>
        /// <response code="204">Consulta generada sin resultados</response>
        [Route("list-filtered")]
        [HttpGet]
        public async Task<IActionResult> HttpGetListData(string categoryId, string typeViewId, string tagsId, string ordenar, string texto)
        {
            Core.Dato dataCore = new Core.Dato();
            var dataFilter = await Task.Run(() => dataCore.GetListDatas(categoryId, typeViewId, tagsId, ordenar, texto));

            if (dataFilter.Count > 0)
            {
                return Ok(new { message = dataFilter });
            }
            else
            {
                return NoContent();
            }
        }
        /// <summary>
        /// Retorna el listado de conjunto de datos.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Consulta generada con  éxito</response>
        /// <response code="204">Consulta generada sin resultados</response>
        [Route("list")]
        [HttpGet]
        public IActionResult HttpGetLstData()
        {

            Dato core = new();
            var lstEntities = core.GetLstData();
            if (lstEntities.Count > 0)
            {
                return Ok(new { message = lstEntities });
            }
            else
            {
                return NoContent();
            }
        }
    }
}
