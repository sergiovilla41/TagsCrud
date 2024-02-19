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
    public class Variable : IBaseVariables
    {
        private  readonly VariableRepo VariablesRepo;

        public Variable() { 

            VariablesRepo= new();
        }

        public List<ConfiguracionVariableDto> GetVariables()
        {
            return VariablesRepo.GetVariables();
        }

        public InventarioVariablesResultDto GetVariablesFilteredByTitle(string texto)
        {
            return VariablesRepo.GetVariablesFilteredByTitle(texto);
        }

        public List<ConfiguracionVariableDto> GetVariableById(Guid id)
        {
            return VariablesRepo.GetVariableById(id);
        }
    }
}
