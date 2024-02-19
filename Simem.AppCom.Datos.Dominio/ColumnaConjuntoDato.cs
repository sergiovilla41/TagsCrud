using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    [Table("ColumnasDestino", Schema = "configuracion")]
    public class ColumnaConjuntoDato
    {
        [Key]
        public Guid IdColumnaDestino { get; set; }
        public string? NombreColumnaDestino { get; set; }
        public string? TipoDato { get; set; }
        public string? AtributoVariable { get; set; }
        public string? VariableId { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        
        public ICollection<ColumnaDatoConjuntoDato>? ColumnaDatoConjuntoDato { get; set; }
    }
}
