using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public interface IVariableRepo
    {
        List<ConfiguracionVariableDto> GetVariables();
        InventarioVariablesResultDto GetVariablesFilteredByTitle(string texto);

        List<ConfiguracionVariableDto> GetVariableById(Guid id);
    }
}
