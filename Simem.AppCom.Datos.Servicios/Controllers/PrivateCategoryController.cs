using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
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
    /// Controlador para gestionar las categorías privadas de SIMEM.
    /// </summary>
    [Route("private-category")]
    public class PrivateCategoryController : ControllerBase
    {
        /// <summary>
        /// Enlistar las subcategoria de las categoías padre (Datos privados)
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Resultado busqueda exitoso</response>
        /// <response code="204">No se encontraron resultados</response>
        /// <response code="400">No se puede procesar la solicitud</response>
        [HttpGet]
        public async Task<IActionResult> Http_GetCategoriesHijosPrivado()
        {
            try
            {
                Categoria categoryCore = new Core.Categoria();
                var categorias = await Task.Run(() => categoryCore.GetCategoryxLevelPrivado());
                if (categorias != null)
                {
                    var result = new JsonResult(new { message = categorias });

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
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
    }
}
