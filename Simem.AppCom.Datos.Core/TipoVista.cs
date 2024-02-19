using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Base.Interfaces;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Datos.Dto;

namespace Simem.AppCom.Datos.Core
{
    public class TipoVista : IBaseTypeView
    {
        private readonly TipoVistaRepo repo;
        public TipoVista()
        {
            repo ??= new TipoVistaRepo();
        }

        public List<TipoVistaDto> GetTypeViews()
        {
            return repo.GetTypeViews();
        }

        public TipoVistaDto GetTypeView(Guid idRegistry)
        {
            return repo.GetTypeView(idRegistry);
        }
        public Task NewTypeView(TipoVistaDto entityDto)
        {
            return repo.NewTypeView(entityDto);
        }
        public Task DeleteTypeView(Guid idRegistry) { return repo.DeleteTypeView(idRegistry); }
        public Task<bool> ModifyTypeView(TipoVistaDto entityDto) { return repo.ModifyTypeView(entityDto); }

    }
}
