using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Dto
{
    [ExcludeFromCodeCoverage]
    public class MegaMenuDto
    {
        public Guid IdMegaMenu { get; set; }
        public string? Titulo { get; set; }
        public string? Icono { get; set; }
        public bool Estado { get; set; }
        public string? Enlace { get; set; }
        public int? OrdenMenu { get; set; }
        public ICollection<MegaMenuSectionDto>? MegaMenuSeccion { get; set; }

        public MegaMenuDto()
        {
            MegaMenuSeccion = new List<MegaMenuSectionDto>();
        }
    }

    public class MegaMenuSectionDto
    {
        public Guid Id { get; set; }
        public Guid? MegaMenuId { get; set; }
        public string? Titulo { get; set; }
        public string? Icono { get; set; }
        public bool Estado { get; set; }
        public int? MegaMenuSeccionOrden { get; set; }
        public ICollection<MegaMenuSeccionDatoDto>? MegaMenuSeccionDato { get; set; }

        public MegaMenuSectionDto()
        {
            MegaMenuSeccionDato = new List<MegaMenuSeccionDatoDto>();
        }
    }

    public class MegaMenuSeccionDatoDto
    {
        public Guid Id { get; set; }
        public Guid? MegaMenuSeccionID { get; set; }
        public Guid? CategoriaID { get; set; }
        public string? Titulo { get; set; }
        public string? Icono { get; set; }
        public bool Estado { get; set; }
        public int OrdenMegaMenuSeccionDato { get; set; }

    }
    


}
