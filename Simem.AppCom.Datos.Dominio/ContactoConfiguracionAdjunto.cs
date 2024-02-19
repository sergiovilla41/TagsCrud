using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Simem.AppCom.Datos.Dominio
{
    [ExcludeFromCodeCoverage]
    [Table("ContactoConfiguracionAdjunto", Schema = "simem")]
    public class ContactoConfiguracionAdjunto
    {
        [Key()]
        public Guid Id { get; set; }
        public string? NombreConfiguracion { get; set; }
        public int CantidadFicheros { get; set; }
        public int Limite { get; set; }
        public string? TipoArchivo { get; set; }
        public int CantidadCaracteresNombre { get; set; }
    }
}
