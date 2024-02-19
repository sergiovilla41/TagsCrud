using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    [Table("ColumnasOrigen", Schema = "configuracion")]
    public class ColumnaDatoConjuntoDato
    {
        [Key]
        public Guid IdColumnaOrigen { get; set; }
        public Guid IdConfiguracionGeneracionArchivos { get; set; }
        public string? NombreColumnaOrigen { get; set; }
        public int NumeracionColumna { get; set; }
        public Guid IdColumnaDestino { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        [ForeignKey("IdColumnaDestino")]
        public ColumnaConjuntoDato? ColumnaConjuntoDato { get; set; }
    }
}
