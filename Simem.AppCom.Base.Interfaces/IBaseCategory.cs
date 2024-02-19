using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using SImem.AppCom.Datos.Dto;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IBaseCategory
    {
        public List<CategoriaDto> GetCategories();
        public CategoriaDto GetCategory(Guid idRegistry);
        public Task NewCategory(CategoriaDto category);
        public Task DeleteCategory(Guid idRegistry);
        public Task<bool> ModifyCategory(CategoriaDto category);
        public List<ListarCategoriaDto> GetCategoriesHijos(bool esPrivado);
        public Task<List<CategoriasHijosPrcResult>> GetCategoryxLevel();
        public List<MigPanPrcResultDto> GetCrumbBreadCategory(Guid IdCategoria);
        public bool UpdateCategories();
        public bool UpdateDownloadsCategories();
    }
}
