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
    /// Controlador para el manejo de las preguntas frecuetnes de SIMEM.
    /// </summary>
    [Route("faqs")]
    public class FaqsController : ControllerBase
    {
        /// <summary>
        /// Elimina una pregunta frecuenta por su id
        /// </summary>
        /// <param name="id">id de la pregunta frecuente.</param>
        /// <returns></returns>
        /// <response code="204">Pregunta eliminada con éxito</response>
        /// <response code="400">Error al eliminar la pregunta</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaqs([BindRequired]  string id)
        {
            try
            {
                Core.PreguntaFrecuente core = new Core.PreguntaFrecuente();
                await core.DeletePreguntasFrecuentes(Convert.ToInt32(id));
                return NoContent(); // Devolver un código de estado 204 (No Content) para indicar eliminación exitosa
            }
            catch (Exception ex)
            {
                return  BadRequest( new { mensajeError = $"Error al eliminar la pregunta frecuente: {ex.Message}" });
            }
        }
        /// <summary>
        /// Obtiene listado de preguntas frecuentes.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Resultado busqueda exitoso</response>
        /// <response code="204">No se encontraron resultados</response>
        /// <response code="400">Error al generar la solicitud</response>
        [HttpGet]
        public async Task<IActionResult> HttpGetPreguntasFrecuentes()
        {
            try
            {
                PreguntaFrecuente core = new Core.PreguntaFrecuente();
                var entity = await Task.Run(() => core.GetPreguntasFrecuentes());
                if (entity != null)
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
        /// Obtiene un registro de preguntas frecuente por id
        /// </summary>
        /// <param name="id">id del registro</param>
        /// <returns></returns>
        /// <response code="200">Resultado busqueda exitoso</response>
        /// <response code="204">No se encontraron resultados</response>
        /// <response code="400">Ocurrio un error al procesar la solicitud</response>
        [HttpGet("{Id}")]
        public async Task<IActionResult> HttpGetPreguntasFrecuentesId([BindRequired]  string id)
        {
            try
            {
                string IdRegistry = id;
                Core.PreguntaFrecuente core = new Core.PreguntaFrecuente();
                var entity = await Task.Run(() => core.GetPreguntasFrecuentes(Convert.ToInt32(IdRegistry)));
                if(entity.Id!=0)
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
                return  BadRequest( new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
        /// <summary>
        /// Guarda una pregunta frecuenta.
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Guardado pregunta frecuente exitoso</response>
        /// <response code="400">Error al guardar una pregunta frecuente</response>
        [HttpPost]
        public async Task<IActionResult> HttpSavePreguntasFrecuentes([FromBody,BindRequired] PreguntasFrecuentesDto entity)
        {
            try
            {
                Core.PreguntaFrecuente core = new();
                if(entity == null)
                {
                    return NoContent();
                }
                await core.NewPreguntasFrecuentes(entity);
                
                return Ok(new { message = "Pregunta Frecuente agregado" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
        /// <summary>
        ///  Actualiza los datos de un registro de pregunta frecuente.
        /// </summary>
        /// <returns></returns>
        /// <returns></returns>
        /// <response code="204">Actualización pregunta frecuente exitoso</response>
        /// <response code="409">Error al actualizar una pregunta frecuente</response>
        /// <response code="400">Error interno del servidor</response>
        [HttpPut]
        public async Task<IActionResult> HttpUpdatePreguntasFrecuentes([FromBody, BindRequired] PreguntasFrecuentesDto entity)
        {
            try
            {
                Core.PreguntaFrecuente core = new Core.PreguntaFrecuente();
                if (entity == null)
                {
                    return NoContent();
                }
                if (!await core.ModifyPreguntasFrecuentes(entity))
                {
                    return StatusCode(409, new { message = "Error al modificar la Pregunta Frecuente" });
                }

                return Ok(new { message = "Pregunta Frecuente modificado" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
    }
}
