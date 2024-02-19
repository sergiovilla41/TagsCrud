using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IConfiguracionVariable
    {
        public Task<List<ConfiguracionVariableDto>> GetDataById(Guid id);
    }
}
