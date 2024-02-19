using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Base.Interfaces;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Datos.Dto;
using SImem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Dominio;

namespace Simem.AppCom.Datos.Core
{
    public class Categoria : IBaseCategory
    {
        private readonly CategoriaRepo categoryrepo;
        public Categoria()
        {
            categoryrepo ??= new CategoriaRepo();
        }

        public List<CategoriaDto> GetCategories()
        {
            return categoryrepo.GetCategories();
        }
        public List<ListarCategoriaDto> GetCategoriesHijos(bool esPrivado)
        {
            return categoryrepo.GetCategoriesHijos(esPrivado);
        }

        public async Task<List<CategoriasHijosPrcResult>> GetCategoryxLevel()
        {
            return await categoryrepo.GetCategoryxLevel();
        }

        public Task<List<CategoriasHijosPrcResult>> GetCategoryxLevelPrivado()
        {
            return categoryrepo.getCategoryxLevelPrivado();
        }

        public CategoriaDto GetCategory(Guid idRegistry)
        {
            return categoryrepo.GetCategory(idRegistry);
        }
        public Task NewCategory(CategoriaDto category)
        {
            return categoryrepo.NewCategory(category);
        }
        public Task DeleteCategory(Guid idRegistry) { return categoryrepo.DeleteCategory(idRegistry); }
        public Task<bool> ModifyCategory(CategoriaDto category) { return categoryrepo.ModifyCategory(category); }

        public List<MigPanPrcResultDto> GetCrumbBreadCategory(Guid IdCategoria)
        {
            return categoryrepo.GetCrumbBreadCategory(IdCategoria);
        }

        public bool UpdateCategories()
        {
            return categoryrepo.UpdateCategories();
        }

        public bool UpdateDownloadsCategories()
        {
            return categoryrepo.UpdateDownloadsCategories();
        }
    }
}
