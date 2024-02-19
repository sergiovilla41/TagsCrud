using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dto;

namespace Simem.AppCom.Datos.Repo
{
    public interface IAgenteRepo
    {
        public List<AgentDto> GetAgents();
        public AgentDto GetAgent(Guid idRegistry);
        public Task newAgent(AgentDto agent);
        public Task DeleteAgent(Guid idRegistry);
        public Task<bool> ModifyAgent(AgentDto agent);
    }
}
