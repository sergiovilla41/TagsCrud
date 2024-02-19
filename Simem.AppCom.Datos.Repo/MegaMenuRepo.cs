using Mapeos;
using Microsoft.EntityFrameworkCore;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Base.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Simem.AppCom.Datos.Repo
{
    public class MegaMenuRepo : IMegaMenuRepo
    {
        private readonly DbContextSimem _baseContext;
        public MegaMenuRepo()
        {
            _baseContext ??= new DbContextSimem();
        }
        public async Task<List<MegaMenuDto>> GetMegaMenuComplete()
        {
            List<MegaMenuDto> megaMenuDto;
            var megaMenu = await _baseContext.MegaMenu.Where(a => a.Estado).OrderBy(c => c.OrdenMenu)
                .ToListAsync();
            megaMenuDto = MapeoDatos.Mapper.Map<List<MegaMenuDto>>(megaMenu);

            for (int i = 0; i < megaMenuDto.Count; i++)
            {
                var sectionsDto = MapeoDatos.Mapper.Map<List<CategoriaDto>>(await _baseContext.Categoria.Where(c => c.IdCategoria == null && !c.privado && c.Estado).OrderBy(c => c.OrdenCategoria).ToListAsync());
                if (megaMenuDto[i].IdMegaMenu.Equals(new Guid("7503E8E7-9D98-4275-AA50-6E9B1A27BCB8")))
                {
                    megaMenuDto[i].MegaMenuSeccion = new List<MegaMenuSectionDto>();

                    foreach (CategoriaDto section in sectionsDto)
                    {
                        var CategoriaDatoDto = MapeoDatos.Mapper.Map<List<CategoriaDto>>(await _baseContext.Categoria.Where(c => c.IdCategoria == section.Id && c.Estado && !c.privado).OrderBy(c => c.OrdenCategoria).ToListAsync());
                        var sectionDatoDto = new List<MegaMenuSeccionDatoDto>();
                        foreach (CategoriaDto sectionDato in CategoriaDatoDto)
                        {
                            sectionDatoDto.Add(new MegaMenuSeccionDatoDto()
                            {
                                Id = Pars(sectionDato.Id?.ToString()),
                                Estado = sectionDato.Estado,
                                Icono = Path.GetFileNameWithoutExtension(sectionDato.Icono),
                                Titulo = sectionDato.Titulo,
                                MegaMenuSeccionID = section.Id,
                                CategoriaID = Pars(sectionDato.Id?.ToString())
                            });
                        }

                        megaMenuDto[i].MegaMenuSeccion?.Add(new MegaMenuSectionDto()
                        {
                            Id = Pars(section.Id?.ToString()),
                            Estado = section.Estado,
                            Icono = Path.GetFileNameWithoutExtension(section.Icono),
                            Titulo = section.Titulo,
                            MegaMenuId = megaMenuDto[i].IdMegaMenu,
                            MegaMenuSeccionOrden = section.OrdenCategoria,
                            MegaMenuSeccionDato = sectionDatoDto
                        });

                    }
                }

            }

            return megaMenuDto;

        }

        private static Guid Pars(string? nullableGuid)
        {
            _ = Guid.Empty;
            if (Guid.TryParse(nullableGuid, out Guid newGuid))
            {
                return newGuid;
            }
            return newGuid;
        }

    }
}
