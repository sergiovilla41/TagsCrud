using Mapeos;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public class ConfiguracionVariableRepo : IConfiguracionVariableRepo
    {
        private readonly DbContextSimem _baseContext;
        public ConfiguracionVariableRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public async Task<List<ConfiguracionVariableDto>> GetDataById(Guid id)
        {
            List<ConfiguracionVariableDto> result = new List<ConfiguracionVariableDto>();
            await Task.Run(() =>
            {
                var dataResult = (from variable in _baseContext.ConfiguracionVariable
                                  join variableXGeneracion in _baseContext.ConfiguracionVariableGeneracionArchivo
                                  on variable.IdVariable equals variableXGeneracion.IdVariable
                                  where variableXGeneracion.IdConfiguracionGeneracionArchivos == id
                                  select variable ).ToList();

                result = MapeoDatos.Mapper.Map<List<ConfiguracionVariableDto>>(dataResult);
            });
            return result;
        }
    }
}
