using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dto;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IBaseTypeView
    {
        public List<TipoVistaDto> GetTypeViews();
        public TipoVistaDto GetTypeView(Guid idRegistry);
        public Task NewTypeView(TipoVistaDto entityDto);
        public Task DeleteTypeView(Guid idRegistry);
        public Task<bool> ModifyTypeView(TipoVistaDto entityDto);
    }
}
