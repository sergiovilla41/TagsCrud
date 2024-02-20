using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Simem.AppCom.Datos.Core;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;

namespace Simem.AppCom.Datos.Servicios.Controllers
{
    /// <summary>
    /// Api para la manipulación de las etiquetas
    /// </summary>
    [Route("tags")]
    public class TagsController : ControllerBase
    {
        /// <summary>
        /// Se utiliza para eliminar una etiqueta
        /// </summary>
        /// <param name="id">Id de la etiqueta</param>
        /// <returns></returns>
        /// <response code="200">La etiqueta se eliminó correctamente</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">Hubo un error en la petición</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(string id)
        {
            try
            {
                Etiqueta core = new Core.Etiqueta();
                await core.DeleteTag(Guid.Parse(id));
                return NoContent(); // Devolver un código de estado 204 (No Content) para indicar eliminación exitosa
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensajeError = $"Error al eliminar el tag: {ex.Message}" });
            }
        }

        /// <summary>
        /// Se utiliza para buscar una etiqueta
        /// </summary>
        /// <param name="Id">Id de la etiqueta</param>
        /// <returns></returns>
        /// <response code="200">La etiqueta correspondiente con el id ingresado</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">Hubo un error en la petición</response>
        [HttpGet("{Id}")]
        public async Task<IActionResult> HttpGetTag(string Id)
        {

            try
            {
                string IdRegistry = Id;
                Etiqueta core = new Etiqueta();
                var entity = await Task.Run(() => core.GetTag(Guid.Parse(IdRegistry)));
                if (entity.Id != null)
                {
                    return Ok(new { message = entity });
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
        /// Se utiliza para obtener todas las etiquetas
        /// </summary>
        /// <returns></returns>
        /// <response code="200">La lista de etiquetas de la aplicación</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">No se puede procesar la solicitud</response>
        [HttpGet]
        public async Task<IActionResult> HttpGetTags()
        {
            try
            {
                Etiqueta core = new Core.Etiqueta();
                var lstEntities = await Task.Run(() => core.GetTags());
                if (lstEntities.Count > 0)
                {
                    return Ok(new { message = lstEntities });
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
        /// Se utiliza para crear una etiqueta
        /// </summary>
        /// <returns></returns>
        /// <response code="200">La etiqueta ingresada se creo correctamente</response>
        /// <response code="400">Hubo un error en la petición</response>
        [HttpPost]
        public async Task<IActionResult> HttpSaveTag([FromBody] EnlaceDto enlaceDto)
        {
            try
            {
                if (enlaceDto == null)
                {
                    return StatusCode(500);
                }
                Etiqueta core = new Etiqueta();
                await core.NewTag(enlaceDto);
                return new OkObjectResult(new { message = "Tag agregado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }

        /// <summary>
        /// Se utiliza para actualizar una etiqueta
        /// </summary>
        /// <returns></returns>
        /// <response code="200">La etiqueta se modificó correctamente</response>
        /// <response code="400">Hubo un error en la petición</response>
        [HttpPut]
        public async Task<IActionResult> HttpUpdateTag([FromBody] EnlaceDto enlaceDto)
        {
            try
            {

                if (enlaceDto == null)
                {
                    return BadRequest();
                }
                Etiqueta core = new Etiqueta();
                if (!await core.ModifyTag(enlaceDto))
                {
                    return new OkObjectResult(new { message = "Error al modificar Tag" });
                }

                return new OkObjectResult(new { message = "Tag modificado" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
        /// <summary>
        /// Se utiliza para llamar un conjunto de datos respecto a la etiqueta selecionada
        /// </summary>
        /// <returns></returns>
        /// <response code="200">La lista de etiquetas de la aplicación con el conjunto de datos asociados</response>
        /// <response code="400">Hubo un error en la petición</response>
        [HttpGet("ALL")]
        public async Task<IActionResult> GetDatos()
        {
            try
            {
                Etiqueta core = new Etiqueta();
                var datosDto = await core.GetDatosDto(); // Llama al método asincrónico
                return Ok(datosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Error: {ex.Message}" });
            }
        }
        /// <summary>
        /// Se utiliza para buscar un conjunto de datos asociados para la etiqueta
        /// </summary>
        /// <param name="id">Id de la tabla GeneracionArchivoEtiqueta</param>
        /// <returns></returns>
        /// <response code="200">La consulta correspondiente con el id ingresado</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">Hubo un error en la petición</response>
        [HttpGet("ById/{id}")]
        public async Task<IActionResult> GetDatosById(Guid id)
        {
            try
            {
                Etiqueta core = new Etiqueta();
                var datosDto = await core.GetDatosDtoById(id);
                return Ok(datosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Error: {ex.Message}" });
            }
        }
        // <summary>
        /// Se utiliza para eliminar una etiqueta con el conjunto de datos asociado       
        /// <param name="id">Id de la tabla GeneracionArchivoEtiqueta</param>
        /// <returns></returns>
        /// <response code="200">El conjunto de datos se eliminó correctamente</response>
        /// <response code="204">No se encontro informacion</response>
        /// <response code="400">Hubo un error en la petición</response>
        [HttpDelete("C-Datos/{id}")]
        public async Task<IActionResult> DeleteDatosById(Guid id)
        {
            try
            {
                Etiqueta core = new Etiqueta();
               await core.DeleteDatosById(id);
                return Ok(new { message = "El conjunto de datos se eliminó correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Error: {ex.Message}" });
            }
        }
        /// <summary>
        /// Se utiliza para crear una etiqueta nueva con el conjunto de datos asociado
        /// </summary>
        /// <returns></returns>
        /// <response code="200">El conjunto de datos se creó correctamente</response>
        /// <response code="400">Hubo un error en la petición</response>
        [HttpPost("Crear")]
        public async Task<IActionResult> CrearDatos([FromBody] ConjuntoDatosDto nuevoConjuntoDatos)
        {
            try
            {
                Etiqueta core = new Etiqueta();
                await core.AddDatosDtoAsync(nuevoConjuntoDatos);
                return Ok(new { message = "El conjunto de datos se creó correctamente" });
            }
            catch (Exception ex)
            {
                // Registra la excepción interna para obtener más detalles sobre el error
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");

                return BadRequest(new { messageError = $"Error: {ex.Message}" });
            }
        }
        /// <summary>
        /// Se utiliza para actualizar una etiqueta nueva con el conjunto de datos asociado
        /// </summary>
        /// <returns></returns>
        /// <response code="200">El conjunto de datos se actualizó correctamente</response>
        /// <response code="400">Hubo un error en la petición</response>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarDatos(ConjuntoDatosDto conjuntoDatosActualizado)
        {
            try
            {
                Etiqueta core = new Etiqueta();
                await core.UpdateDatosDto( conjuntoDatosActualizado);
                return Ok(new { message = "El conjunto de datos se actualizó correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { messageError = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Error: {ex.Message}" });
            }
        }


    }

}



