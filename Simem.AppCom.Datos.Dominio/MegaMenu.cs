using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    [Table("MegaMenu", Schema = "menu")]
    public class MegaMenu
    {
        [Key()]
        public Guid IdMegaMenu { get; set; }
        [Required(ErrorMessage = "El campo Title es requerido", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "El campo Title no puede tener mas de {1} caracteres")]
        public string? Titulo { get; set; }
        [StringLength(30, ErrorMessage = "El campo Icon no puede tener mas de {1} caracteres")]
        public string? Icono { get; set; }
        [Required(ErrorMessage = "El campo State es requerido", AllowEmptyStrings = false)]
        public bool Estado { get; set; }
        public int? OrdenMenu { get; set; }
        public string? Enlace { get; set; }
    }
}
