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
    /// Controlador que maneja eventos del menú de interés.
    /// </summary>
    [Route("menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        /// <summary>
        /// Carga las opciones del menú.
        /// </summary>
        /// <response code="200">Lista de registros del menu devuelta con éxito</response>
        /// <response code="204">No se encontraron registros</response>
        /// <response code="400">No se puede procesar la solicitud</response>
        [HttpGet]
        public async Task<IActionResult> HttpGetMegaMenu()
        {
            try
            {
                MegaMenu megaMenuCore = new();
                List<MegaMenuDto> usuarios = await megaMenuCore.GetMegaMenuComplete();
                if(!usuarios.Any()) return NoContent();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { messageError = $"Ocurrio un error : " + ex.Message });
            }
        }
    }
}
