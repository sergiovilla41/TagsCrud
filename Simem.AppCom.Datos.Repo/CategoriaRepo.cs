using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using SImem.AppCom.Datos.Dto;
using Mapeos;

namespace Simem.AppCom.Datos.Repo
{
    public class CategoriaRepo : ICategoriaRepo
    {
        private readonly DbContextSimem _baseContext;

        public CategoriaRepo()
        {
            _baseContext ??= new DbContextSimem();
        }
        public List<CategoriaDto> GetCategories()
        {

            List<Datos.Dto.CategoriaDto> lstCategories = new List<CategoriaDto>();
            var dbCategories = _baseContext.Categoria.Where(a => !a.privado && a.Estado).Where(x => x.IdCategoria == null).ToList();
            foreach (var item in dbCategories)
            {
                var hijos = _baseContext.Categoria.Where(x => x.IdCategoria == item.Id).Select(y => y.Id);
                var nietos = _baseContext.Categoria.Where(x => hijos.Contains(x.IdCategoria)).Select(x => x.Id);
                var dbLastDataUpdate = _baseContext.GeneracionArchivo.Where(x => (x.IdCategoria == item.Id || hijos.Contains(x.IdCategoria)) || nietos.Contains(x.IdCategoria) && x.UltimaFechaActualizado.HasValue).OrderByDescending(y => y.UltimaFechaActualizado).FirstOrDefault();
                CategoriaDto cat = new CategoriaDto();
                cat.Id = item.Id;
                cat.Icono = item.Icono;
                cat.Titulo = item.Titulo;
                cat.Estado = item.Estado;
                cat.Descripcion = item.Descripcion;
                cat.ConjuntoDato = item.CantidadConjuntoDato;
                cat.Descarga = item.CantidadDescarga.ToString();
                cat.UltimaActualizacion = dbLastDataUpdate?.UltimaFechaActualizado?.ToString("yyyy-MM-dd HH:mm");
                cat.UltimaActualizacionDatoTitulo = dbLastDataUpdate?.Titulo;
                cat.UltimaActualizacionDatoId = dbLastDataUpdate?.IdConfiguracionGeneracionArchivos;

                lstCategories.Add(cat);
            }
            return lstCategories;
        }

        public CategoriaDto GetCategory(Guid? idRegistry)
        {
            var dbCategory = _baseContext.Categoria.FirstOrDefault(c => c.Id.Equals(idRegistry));
            CategoriaDto cat = new CategoriaDto();
            if (dbCategory != null)
            {
                cat.Id = dbCategory.Id;
                cat.Titulo = dbCategory.Titulo;
                cat.Icono = dbCategory.Icono;
                cat.Estado = dbCategory.Estado;
                cat.Descripcion = dbCategory.Descripcion;
            }

            return cat;
        }

        public async Task NewCategory(CategoriaDto category)
        {
            var transaction = _baseContext.Database.BeginTransaction();

            Categoria cat = new Categoria();
            cat.Estado = true;
            cat.Icono = category.Icono;
            cat.Titulo = category.Titulo;
            cat.Descripcion = category.Descripcion;
            cat.Id = Guid.NewGuid();
            cat.IdCategoria = cat.Id;
            cat.GeneracionArchivo = null;
            cat.OrdenCategoria = category.OrdenCategoria;
            try
            {
                _baseContext.Categoria.Add(cat);
                await _baseContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }


        public async Task DeleteCategory(Guid idRegistry)
        {
            var dbCategory = _baseContext.Categoria.FirstOrDefault(c => c.Id.Equals(idRegistry));
            var transaction = _baseContext.Database.BeginTransaction();
            try
            {
                if (dbCategory != null)
                {
                    _baseContext.Categoria.Remove(dbCategory);
                    await _baseContext.SaveChangesAsync();
                    transaction.Commit();
                }

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> ModifyCategory(CategoriaDto category)
        {
            var transaction = _baseContext.Database.BeginTransaction();
            bool response = false;
            var cat = _baseContext.Categoria.FirstOrDefault(x => x.Id.Equals(category.Id));

            try
            {
                if (cat != null)
                {
                    cat.Estado = category.Estado;
                    cat.Icono = category.Icono;
                    cat.Titulo = category.Titulo;
                    cat.Descripcion = category.Descripcion;
                    await _baseContext.SaveChangesAsync();
                    transaction.Commit();
                    response = true;
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            return response;
        }

        public async Task<List<CategoriasHijosPrcResult>> GetCategoryxLevel()
        {
            return await _baseContext.CategoriasHijosPrcResult.FromSqlRaw("[simem].[BuscarCategoriasNNiveles]").ToListAsync();
        }

        public async Task<List<CategoriasHijosPrcResult>> getCategoryxLevelPrivado()
        {
             return await _baseContext.CategoriasHijosPrcResult.FromSqlRaw("[simem].[BuscarCategoriasNNivelesPrivado]").ToListAsync();
        }

        public List<ListarCategoriaDto> GetCategoriesHijos(bool esPrivado)
        {
            List<Datos.Dto.ListarCategoriaDto> lstCategories = new List<ListarCategoriaDto>();
            var dbCategories = _baseContext.Categoria.Where(x => x.IdCategoria == null && x.privado == esPrivado).ToList();
            foreach (var item in dbCategories)
            {
                ListarCategoriaDto cat = new ListarCategoriaDto();
                cat.Id = item.Id;
                cat.Icono = item.Icono;
                cat.Titulo = item.Titulo;
                cat.Estado = item.Estado;
                cat.Descripcion = item.Descripcion;
                cat.ConjuntoDato = _baseContext.GeneracionArchivo.Where(x => x.IdCategoria == item.Id).Count();
                cat.ListaHijosDto = GetCategoryHijos(item.Id);
                lstCategories.Add(cat);
            }

            return lstCategories;
        }

        public List<Datos.Dto.CategoryHijosDto> GetCategoryHijos(Guid? Id)
        {
            List<Datos.Dto.CategoryHijosDto> lstCategoriesHijos = new List<CategoryHijosDto>();
            var dbCategories = _baseContext.Categoria.Where(x => x.IdCategoria == Id).OrderBy(o => o.OrdenCategoria).ToList();
            foreach (var item in dbCategories)
            {
                CategoryHijosDto cat = new CategoryHijosDto();
                cat.Id = item.Id;
                cat.Icono = item.Icono;
                cat.Titulo = item.Titulo;
                cat.Estado = item.Estado;
                cat.CategoriaID = item.IdCategoria;
                cat.Descripcion = item.Descripcion;
                cat.ConjuntoDato = _baseContext.GeneracionArchivo.Where(x => x.IdCategoria == item.Id).Count();
                lstCategoriesHijos.Add(cat);
            }

            return lstCategoriesHijos;
        }

        public List<MigPanPrcResultDto> GetCrumbBreadCategory(Guid IdCategoria)
        {
            List<MigPanPrcResultDto> result = new();
            try
            {

                var resultado = _baseContext.MigaPanPrcResult.FromSqlInterpolated($" EXEC [simem].[prc_obtenerMigaPanCategoria] {IdCategoria}").ToList();

                if (resultado.Count > 0)
                {
                    var dto = MapeoDatos.Mapper.Map<List<MigPanPrcResultDto>>(resultado);
                    return dto;
                }
                return result;

               
            }
            catch (Exception)
            {
                return result;
            }
        }

        public bool UpdateCategories()
        {
            try
            {
               _ = _baseContext.Database.ExecuteSqlInterpolated($"[simem].[prc_contadorCategoriaNivel1]");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateDownloadsCategories()
        {
            try
            {
               _ = _baseContext.Database.ExecuteSqlInterpolated($"[simem].[prc_contadorDescargasCategoriaNivel1]");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
