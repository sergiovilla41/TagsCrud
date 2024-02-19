using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    [Table("Etiqueta")]
    public class Etiqueta
    {
        [Key()]
        public Guid? Id { get; set; }
        public string? Titulo { get; set; }
        public bool Estado { get; set; }      

    }
}
