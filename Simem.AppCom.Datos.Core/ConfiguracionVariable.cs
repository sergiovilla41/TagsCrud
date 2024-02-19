using Simem.AppCom.Base.Interfaces;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Core
{
    public class ConfiguracionVariable : IConfiguracionVariable
    {
        private readonly ConfiguracionVariableRepo repository;
        public ConfiguracionVariable()
        {
            repository ??= new ConfiguracionVariableRepo();
        }

        public async Task<List<ConfiguracionVariableDto>> GetDataById(Guid id)
        {
            return await repository.GetDataById(id);
        }
    }
}
