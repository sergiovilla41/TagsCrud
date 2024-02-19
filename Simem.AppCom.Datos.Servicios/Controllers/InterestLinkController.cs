using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;

namespace Simem.AppCom.Datos.Servicios.Controllers
{
    /// <summary>
    /// Controlador para manejar eventos de los enlaces de interés de SIMEM.
    /// </summary>
    [Route("interest-links")]
    public class InterestLinkController : ControllerBase
    {
        /// <summary>
        /// Elimina un enlace de interes por id. 
        /// </summary>
        /// <param name="id">id de enlace de interés</param>
        /// <response code="204">Eliminado exitoso</response>
        /// <response code="400">Mensaje de error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterestLink(string id)
        {
            try
            {
                Core.EnlaceInteres core = new Core.EnlaceInteres();
                await core.DeleteEnlaceInteres(Convert.ToInt32(id));

                return NoContent(); // Devolver un código de estado 204 (No Content) para indicar eliminación exitosa
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = $"Error al eliminar el enlace de interés: {ex.Message}" });
            }
        }
        /// <summary>
        /// Obtiene listado de enlaces de interés.
        /// </summary>
        /// <param name="paginator">Configuración del paginador desde el front.</param>
        /// <response code="200">Lista de enlaces de interes devuelta con éxito</response>
        /// <response code="400">Error al generar la solicitud</response>
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> HttpGetEnlaceInteres(string? paginator)
        {
            try
            {
                Paginador? _paginador;
                Core.EnlaceInteres core = new();
                if (!string.IsNullOrEmpty(paginator))
                {
                    _paginador = JsonConvert.DeserializeObject<Paginador>(paginator);
                }
                else
                {
                    var result = await core.GetEnlaceInteres();
                    return Ok(new
                    {
                        message = result,
                        rows = await core.GetEnlaceinteresCount()
                    });
                }

                var entity = await core.GetEnlaceInteres(_paginador!);
                int rows = await core.GetEnlaceinteresCount();
                return Ok(new { message = entity, rows });
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
            
        }
        /// <summary>
        /// Obtiene datalle de enlace de interés por id
        /// </summary>
        /// <param name="id">id de enlace de interés.</param>
        /// <response code="200">Registro enlace de interes devuelto con éxito</response>
        /// <response code="204">Registro enlace de interes no encontrado</response>
        /// <response code="400">Error al generar la solicitud</response>
        [HttpGet]
        public async Task<IActionResult> HttpGetEnlaceInteresId(string id)
        {
            try
            {
                string IdRegistry = id;
                Core.EnlaceInteres core = new Core.EnlaceInteres();
                var entity = await Task.Run(() => core.GetEnlaceInteres(Convert.ToInt32(IdRegistry)));
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
        /// Guarda un registro de enlace de interés
        /// </summary>
        /// <response code="200">Registro creado con éxito</response>
        /// <response code="400">Error durante el proceso de creacion</response>
        [HttpPost]
        public async Task<IActionResult> HttpSaveEnlaceInteres([BindRequired, FromBody] EnlaceInteresDto entity)
        {
            try
            {
                if (entity == null)
                {
                    return NoContent();
                }
                Core.EnlaceInteres core = new Core.EnlaceInteres();
                await core.NewEnlaceInteres(entity);
                
                return new OkObjectResult(new { message = "Enlace Interes agregado" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
        /// <summary>
        /// Actualiza un registro de enlace de interés.
        /// </summary>
        /// <response code="200">Registro modificado con éxito</response>
        /// <response code="400">Error durante el proceso de modificacion</response>
        [HttpPut]
        public async Task<IActionResult> HttpUpdateEnlaceInteres([BindRequired, FromBody] EnlaceInteresDto entity)
        {
            try
            {
                if (entity == null)
                {
                    return NoContent();
                }
                Core.EnlaceInteres core = new();
                await core.ModifyEnlaceInteres(entity);
                
                return Ok(new { message = "Enlace Interes modificado" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
    }
}
