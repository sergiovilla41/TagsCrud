using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Simem.AppCom.Base.Utils;
using Simem.AppCom.Datos.Core;
using Simem.AppCom.Datos.Dominio;
using System.Diagnostics.CodeAnalysis;

namespace Simem.AppCom.Datos.Servicios.Controllers
{
    /// <summary>
    /// Api buscador general
    /// </summary>
    [Route("search")]
    public class SearchController : Controller
    {
        /// <summary>
        /// Buscador transversal o buscador general de Simem.
        /// </summary>
        /// <param name="fail">Determina si lanza una exepcion (Solo pruebas)</param>
        /// <param name="filtro">Dato a filtrar</param>
        /// <param name="tipoContenido">Tipo de contenido a filtrar</param>
        /// <param name="tagsId">Id de etiqueta seleccionada</param>
        /// <response code="200">Devuelve un registro consultado con éxito</response>
        /// <response code="204">No se encuentra información</response>
        /// <response code="400">Error durante el proceso de consulta</response>
        [Route("filter-data")]
        [HttpGet]
        public async Task<IActionResult> HttpGetBuscadorGeneral(string? fail, string? filtro, string? tipoContenido, string? tagsId)
        {
            try
            {
                _ = Convert.ToBoolean(fail);
                BuscadorGeneral buscadorGeneral = new Core.BuscadorGeneral();
                var resultado = await Task.Run(() => buscadorGeneral.BuscarDatos(filtro!, tipoContenido!, tagsId!));
                var resultadoJson = JsonConvert.DeserializeObject<dynamic>(resultado.resultadoJson!);
                var etiquetas = JsonConvert.DeserializeObject<dynamic>(resultado.Etiquetas!);
                if (resultado.totalFilas > 0)
                {
                    var message = new List<object>
                    {
                         new { totalFilas = resultado.totalFilas, resultadosJSON = resultadoJson, etiquetas},
                    };
                    var result = new JsonResult(new { message = message });

                    return new ContentResult
                    {
                        Content = JsonUtilConverter.FormatJsonResult(result.Value ?? ""),
                        ContentType = "application/json",
                    };
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }

        /// <summary>
        /// Busqueda conjunto de datos
        /// </summary>
        /// <param name="categoryId">id de la categoria</param>
        /// <param name="typeViewId">id tipo vista</param>
        /// <param name="tagsId">id etiquetas</param>
        /// <param name="varsId">id variables</param>
        /// <param name="ordenar">ordenamiento de resultados</param>
        /// <param name="texto">dato a buscar</param>
        /// <response code="200">Devuelve un registro consultado con éxito</response>
        /// <response code="204">No se encuentra información</response>
        /// <response code="400">No se puede procesar la solicitud</response>
        [Route("dataset-filter")]
        [HttpGet]
        public async Task<IActionResult> HttpGetDataSets(string? categoryId, string? typeViewId, string? tagsId, string? varsId, string? ordenar, string? texto)
        {
            try
            {
                BuscadorGeneral buscadorGeneral = new Core.BuscadorGeneral();
                var resultado = await Task.Run(() => buscadorGeneral.BuscarDatosConjuntoDatos(categoryId!, typeViewId!, tagsId!, varsId!, texto!, ordenar!, false));
                var resultadoJson = JsonConvert.DeserializeObject<dynamic>(resultado.resultadoJson!);

                if (resultado.totalFilas > 0)
                {
                    var message = new List<object>();
                    if (resultado.etiquetas != null && resultado.variables != null)
                    {
                        var etiquetas = JsonConvert.DeserializeObject<dynamic>(resultado.etiquetas!);
                        var variables = JsonConvert.DeserializeObject<dynamic>(resultado.variables!);
                        message = new List<object>
                  {
                       new { totalFilas = resultado.totalFilas, resultadoJson = resultadoJson, etiquetas = etiquetas,variables = variables },

                  };
                    }

                    if (resultado.etiquetas == null && resultado.variables != null)
                    {
                        var variables = JsonConvert.DeserializeObject<dynamic>(resultado.variables!);
                        message = new List<object>
                     {
                           new { totalFilas = resultado.totalFilas, resultadoJson = resultadoJson, etiquetas = new List<object>(),variables = variables }

                     };
                    }

                    if (resultado.etiquetas != null && resultado.variables == null)
                    {
                        var etiquetas = JsonConvert.DeserializeObject<dynamic>(resultado.etiquetas!);
                        message = new List<object>
                      {
                            new { totalFilas = resultado.totalFilas, resultadoJson = resultadoJson, etiquetas = etiquetas,variables = new List<object>() }
                      };
                    }

                    if (resultado.etiquetas == null && resultado.variables == null)
                    {
                        message = new List<object>
                      {
                            new { totalFilas = resultado.totalFilas, resultadoJson = resultadoJson, etiquetas = new List<object>(),variables = new List<object>() }
                      };
                    }

                    var result = new JsonResult(new { message = message });

                    return new ContentResult
                    {
                        Content = JsonUtilConverter.FormatJsonResult(result.Value!),
                        ContentType = "application/json",
                    };
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
        /// Obtiene los registros de conjunto de datos privados
        /// </summary>
        /// <response code="200">Lista de datasets privados consultados con éxito</response>
        /// <response code="204">No se encuentra información</response>
        /// <response code="400">Error durante el proceso de consulta</response>
        [ExcludeFromCodeCoverage]
        [Route("private-dataset")]
        [HttpGet]
        public async Task<IActionResult> HttpGetDataSetsPrivado()
        {
            try
            {
                string authorization = "";

                if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
                {
                    authorization = authHeaderValue.ToString().Substring(7);
                }

                string email = GraphManagerAuth.ValidateToken(authorization);


                BuscadorGeneral buscadorGeneral = new();
                var resultado = await Task.Run(() => buscadorGeneral.BuscarDatosConjuntoDatosPrivados(email));


                if (resultado != null && resultado != "")
                {
                    var jsonResultadosFiltros = JsonConvert.DeserializeObject<List<dynamic>>(resultado);

                    var result = new JsonResult(new { message = jsonResultadosFiltros });

                    return new ContentResult
                    {
                        Content = JsonUtilConverter.FormatJsonResult(result.Value ?? ""),
                        ContentType = "application/json",
                    };
                }
                else
                {
                    return NoContent();
                }


            }
            catch (Exception)
            {
                return  BadRequest( "error validando la información");
            }
        }
    }
}
