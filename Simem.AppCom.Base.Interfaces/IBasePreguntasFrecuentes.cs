using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dominio;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IBasePreguntasFrecuentes
    {
        public Task<List<PreguntaFrecuente>> GetPreguntasFrecuentes();
        public PreguntasFrecuentesDto GetPreguntasFrecuentes(int id);
        public Task NewPreguntasFrecuentes(PreguntasFrecuentesDto entityDto);
        public Task DeletePreguntasFrecuentes(int id);
        public Task<bool> ModifyPreguntasFrecuentes(PreguntasFrecuentesDto entityDto);
    }
}
