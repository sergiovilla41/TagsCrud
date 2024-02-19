using Mapeos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public class NovedadRepo : INovedadRepo
    {
        private readonly DbContextSimem _baseContext;
        public NovedadRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public async Task<List<NovedadDetail>> GetNovedadesCategoriaNovedades(Paginador paginador, string? term, Guid? category, Guid? idGeneracionArchivo)
        {
            var toDay = DateTime.Now.ToUniversalTime().AddHours(-5.0);
            var novedades = _baseContext.Novedad.Where(a => a.fechaPublicacion <= toDay && a.estado).Include(a => a.CategoriaNovedad);

            if (!string.IsNullOrEmpty(term))
            {
                novedades = novedades.Where(a => EF.Functions.Like(EF.Functions.Collate(a.Titulo ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(term, "Latin1_general_CI_AI") + "%")
                || EF.Functions.Like(EF.Functions.Collate(a.Descripcion ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(term, "Latin1_general_CI_AI") + "%")
                || EF.Functions.Like(EF.Functions.Collate(a.CategoriaNovedad!.Titulo ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(term, "Latin1_general_CI_AI") + "%")
                ).Include(a => a.CategoriaNovedad);
            }

            if (!(category == null || category == Guid.Empty))
            {
                if (idGeneracionArchivo != Guid.Empty)
                {
                    novedades = novedades.Where(a => a.IdCategoriaNovedad == category && a.CategoriaNovedad!.Titulo!.Equals("Datos") && a.IdGeneracionArchivo.Equals(idGeneracionArchivo)).Include(a => a.CategoriaNovedad);
                }
                else
                {
                    novedades = novedades.Where(a => a.IdCategoriaNovedad == category).Include(a => a.CategoriaNovedad);
                }
            }

            novedades = novedades.OrderByDescending(a => a.fechaPublicacion)
                .Skip(paginador.PageIndex * paginador.PageSize)
                .Take(paginador.PageSize).Include(a => a.CategoriaNovedad);

            var novedadeslList = await novedades.ToListAsync();
            List<NovedadDetail> novedadDetailList = new();
            var novedadesEtiquetas = await _baseContext.NovedadEtiquetas.Include(a => a.Etiqueta).ToListAsync();

            novedadeslList.ForEach(a =>
            {
                List<Etiqueta> etiquetas = new();
                List<NovedadEtiqueta> aux = new();
                novedadesEtiquetas.ForEach(b => {
                    if (b.IdNovedad == a.Id)
                    {
                        aux.Add(b);
                    }
                });
                aux.ForEach(novedadesEtiquetas =>
                {
                    if (novedadesEtiquetas.Etiqueta != null)
                    {
                        etiquetas.Add(novedadesEtiquetas.Etiqueta);
                    }

                });

                novedadDetailList.Add(new NovedadDetail() { Novedad = a, Etiquetas = etiquetas });
            });


            return novedadDetailList;
        }
        
        public async Task<NovedadDetail> GetNovedadDetail(Guid Id)
        {
            try {
                var novedad = await _baseContext.Novedad.Where(a => a.Id.Equals(Id)).FirstAsync();
                var novedadesEtiquetas = await _baseContext.NovedadEtiquetas.Include(a => a.Etiqueta).Where(a => a.IdNovedad == Id).ToListAsync();
                List<Etiqueta> etiquetas = new();
                novedadesEtiquetas.ForEach(novedadesEtiquetas =>
                {
                    if (novedadesEtiquetas.Etiqueta != null)
                    {
                        etiquetas.Add(novedadesEtiquetas.Etiqueta);
                    }

                });
                return new NovedadDetail() { Novedad = novedad, Etiquetas = etiquetas };
            }
            catch (Exception) {
                return new NovedadDetail();
            }
        }

        public async Task<List<CategoriaNovedad>> GetNovedadesCategories()
        {
            return await _baseContext.CategoriaNovedad.OrderBy(a => a.OrdenCategoria).ToListAsync();
        }

        public async Task<List<GeneracionArchivo>> GetConjuntoDeDatosConNovedades()
        {
            var novedades = await _baseContext.Novedad.Where(a => a.IdGeneracionArchivo != null && a.CategoriaNovedad!.Titulo!.Equals("Datos")).ToListAsync();

            List<GeneracionArchivo> generacionArchivo = new();
            novedades.ForEach(novedades =>
            {
                generacionArchivo.Add(_baseContext.GeneracionArchivo.Where(a => a.IdConfiguracionGeneracionArchivos == novedades.IdGeneracionArchivo).FirstOrDefault()!);
            });

            return generacionArchivo.Distinct().ToList();
        }

        public async Task<int> GetNovedadesCount(Paginador paginador, string? term, Guid? category, Guid? idGeneracionArchivo)
        {
            var toDay = DateTime.Now.ToUniversalTime().AddHours(-5.0);
            var novedades = _baseContext.Novedad.Where(a => a.fechaPublicacion <= toDay && a.estado).Include(a => a.CategoriaNovedad);

            if (!string.IsNullOrEmpty(term))
            {
                novedades = novedades.Where(a => EF.Functions.Like(EF.Functions.Collate(a.Titulo ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(term, "Latin1_general_CI_AI") + "%")
                || EF.Functions.Like(EF.Functions.Collate(a.Descripcion ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(term, "Latin1_general_CI_AI") + "%")
                || EF.Functions.Like(EF.Functions.Collate(a.CategoriaNovedad!.Titulo ?? "", "Latin1_general_CI_AI"), "%" + EF.Functions.Collate(term, "Latin1_general_CI_AI") + "%")
                ).Include(a => a.CategoriaNovedad);
            }

            if (!(category == null || category == Guid.Empty))
            {
                if (idGeneracionArchivo != Guid.Empty)
                {
                    novedades = novedades.Where(a => a.IdCategoriaNovedad == category && a.CategoriaNovedad!.Titulo!.Equals("Datos") && a.IdGeneracionArchivo.Equals(idGeneracionArchivo)).Include(a => a.CategoriaNovedad);
                }
                else
                {
                    novedades = novedades.Where(a => a.IdCategoriaNovedad == category).Include(a => a.CategoriaNovedad);
                }
            }

            return await novedades.CountAsync();
        }

    }
}
