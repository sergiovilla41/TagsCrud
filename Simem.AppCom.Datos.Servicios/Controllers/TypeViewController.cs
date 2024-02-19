using Microsoft.AspNetCore.Mvc;
using Simem.AppCom.Datos.Core;
using Simem.AppCom.Datos.Dto;

namespace Simem.AppCom.Datos.Servicios.Controllers
{
    /// <summary>
    /// Api dedicada a la manipulación de los tipo de vista de SIMEM
    /// </summary>
    [Route("type-views")]
    public class TypeViewController : ControllerBase
    {
        /// <summary>
        /// Se utiliza para eliminar un tipo de vista que coincida con el id
        /// </summary>
        /// <param name="id">Id del tipo de vista</param>
        /// <returns></returns>
        /// <response code="204">el tipo de vista se eliminó correctamente</response>
        /// <response code="400">Ocurrió un error eliminando el tipo de vista</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeView(string id)
        {
            try
            {
                TipoVista core = new Core.TipoVista();
                await core.DeleteTypeView(Guid.Parse(id));
                return NoContent(); // Devolver un código de estado 204 (No Content) para indicar eliminación exitosa
            }
            catch (Exception ex)
            {
                return  BadRequest( new { mensajeError = $"Error al eliminar el Tipo de Vista: {ex.Message}" });
            }
        }

        /// <summary>
        /// Se utiliza para bucar un tipo de vista específico
        /// </summary>
        /// <param name="fail">Utilizado para obligar a la aplicacióna a fallar</param>
        /// <param name="Id">Id del tipo de vista</param>
        /// <returns></returns>
        /// <response code="200">El tipo de vista correspondiente al id ingresado</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">Ocurrió un error obteniendo los tipo de vista</response>
        [HttpGet("{Id}/{fail?}")]
        public async Task<IActionResult> HttpGetTypeView(string? fail, string Id)
        {

            try
            {
                _ = Convert.ToBoolean(fail);

                string IdRegistry = Id;
                TipoVista core = new();
                var entity = await Task.Run(() => core.GetTypeView(Guid.Parse(IdRegistry)));
                if(entity!=null)
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
        /// Se utiliza para obtener todos los tipos de vista
        /// </summary>
        /// <returns></returns>
        /// <response code="200">el tipo de vista se eliminó correctamente</response>
        /// <response code="204">No se encontre informacion</response>
        /// <response code="400">No se puede procesar la solicitud</response>
        [HttpGet]
        public async Task<IActionResult> HttpGetTypeViews()
        {
            try
            {
                TipoVista core = new TipoVista();
                var lstEntities = await Task.Run(() => core.GetTypeViews());

                if (lstEntities.Count > 0)
                {
                    return Ok(new { message = lstEntities });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex )
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }

        /// <summary>
        /// Se utiliza para crear un tipo de vista
        /// </summary>
        /// <returns></returns>
        /// <response code="200">el tipo de vista se creo correctamente</response>
        /// <response code="400">Ocurrió un error creando el tipo de vista</response>
        [HttpPost]
        public async Task<IActionResult> HttpSaveTypeView([FromBody] TipoVistaDto tipoVistaDto)
        {
            try
            {
              
                    if (tipoVistaDto == null)
                    {
                        return StatusCode(500);
                    }
                    TipoVista core = new Core.TipoVista();
                    await core.NewTypeView(tipoVistaDto);
               
                return Ok(new { message = "TypeView agregada" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }

        /// <summary>
        /// Se utiliza para actualizar un tipo de vista
        /// </summary>
        /// <returns></returns>
        /// <response code="200">el tipo de vista se actualizó correctamente</response>
        /// <response code="400">Ocurrió un error actualizando el tipo de vista</response>
        [HttpPut]
        public async Task<IActionResult> HttpUpdateTypeView(TipoVistaDto tipoVistaDto)
        {
            try
            {
                    if(tipoVistaDto == null)
                    {
                        return StatusCode(500);
                    }
                    TipoVista core = new();
                    if (!await core.ModifyTypeView(tipoVistaDto) && !tipoVistaDto.fail)
                    {
                        return new OkObjectResult(new { message = "Error al modificar TypeView" });
                    }
                return new OkObjectResult(new { message = "TypeView modificado" });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
    }   
}
