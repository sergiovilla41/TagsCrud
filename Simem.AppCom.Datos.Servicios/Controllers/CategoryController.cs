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
    /// Controlador para gestionar las categorías de SIMEM.
    /// </summary>
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// Obtiene el listado de las categorías padres
        /// </summary>
        /// <response code="200">Lista de categorias</response>
        /// <response code="204">No se encuentran categorias</response>
        /// <response code="400">No se encuentran categorias</response>
        [HttpGet]
        public async Task<IActionResult> HttpGetCategories()
        {
            try
            {
                Categoria categoryCore = new();
                var categories = await Task.Run(() => categoryCore.GetCategories());

                if (categories != null && categories.Count > 0)
                {
                    return Ok(new { message = categories });
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
        /// Elimina categoría por el id enviado por parametro.
        /// </summary>
        /// <param name="id">id de la categoría</param>
        /// <returns></returns>
        /// <response code="204">Categoria eliminada con éxito</response>
        /// <response code="400">Error al eliminar la categoria</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([BindRequired] string id)
        {
            try
            {
                Categoria categoryCore = new Categoria();
                await categoryCore.DeleteCategory(Guid.Parse(id));

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Error al eliminar la categoría: {ex.Message}" });
            }
        }
        /// <summary>
        /// Enlistar las subcategoria de las categoías padre.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Resultado busqueda exitoso</response>
        /// <response code="204">No se encontraron resultados</response>
        // <response code="400">Ha ocurrido un error en la consulta</response>
        [Route("sons")]
        [HttpGet]
        public async Task<IActionResult> Http_GetCategoriesHijos()
        {
            try
            {
                Categoria categoryCore = new Core.Categoria();
                var categorias = await Task.Run(() => categoryCore.GetCategoryxLevel());

                if (categorias != null)
                {
                    return Ok(new { Message = categorias });
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
        /// Obtiene una categoría filtrada por su id.
        /// </summary>
        /// <param name="id">id de la ccategoría</param>
        /// <returns></returns>
        /// <response code="200">Resultado busqueda exitoso</response>
        /// <response code="204">No se encontraron resultados</response>
        /// <response code="400">Error al generar la búsqueda</response>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Http_GetCategory([BindRequired] string id)
        {
            try
            {
                string IdRegistry = id;
                Categoria categoryCore = new Core.Categoria();
                var category = await Task.Run(() => categoryCore.GetCategory(Guid.Parse(IdRegistry)));
                if (category.Id == null)
                {
                    return NoContent();
                }
                return Ok(new { message = category });
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
        /// <summary>
        /// Obtiene la miga de pan de la categoría filtrada por el id.
        /// </summary>
        /// <param name="id">id de la categoría</param>
        /// <returns></returns>
        /// <response code="200">Resultado busqueda exitoso</response>
        /// <response code="204">No se encontraron resultados</response>
        /// <response code="400">Error al generar la búsqueda</response>
        [Route("bread-crumb-category")]
        [HttpGet]
        public async Task<IActionResult> HttpGetMigaPanCategoria([BindRequired] string id)
        {
            try
            {
                Categoria categoryCore = new Core.Categoria();
                var categories = await Task.Run(() => categoryCore.GetCrumbBreadCategory(Guid.Parse(id)));
                if (categories.Count > 0)
                {

                    var result = new JsonResult(new { message = categories });

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

        /// <summary>
        /// Guarda una categoría
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Guardado de categoría exitoso</response>
        /// <response code="204">No se encuentra categoria</response>
        /// <response code="500">Error al guardar la categoría</response>
        [HttpPost]
        public async Task<IActionResult> HttpSaveCategory([FromBody, BindRequired] CategoriaDto category)
        {
            try
            {
                if (category != null)
                {
                    Categoria categoryCore = new();
                    await categoryCore.NewCategory(category);
                    return Ok(new { message = "Categoria agregada" });
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
        /// Actualiza una categoría
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Actualización de categoría exitoso</response>
        /// <response code="409">Error al modificar Categoria</response>
        /// <response code="400">Error en el servidor al modificar Categoría</response>
        [HttpPut]
        public async Task<IActionResult> HttpUpdateCategory([FromBody, BindRequired] CategoriaDto category)
        {
            try
            {
                if (category != null)
                {
                    Categoria categoryCore = new();
                    if (!await categoryCore.ModifyCategory(category))
                    {
                        return StatusCode(409, new { message = "Error al modificar Categoria" });
                    }
                }

                return Ok(new { message = "Categoria modificada" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }

    }
}


