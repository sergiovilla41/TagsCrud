using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public interface IConfiguracionVariableRepo
    {
        public Task<List<ConfiguracionVariableDto>> GetDataById(Guid id);
    }
}
