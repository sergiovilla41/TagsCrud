using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dominio;

namespace Simem.AppCom.Datos.Repo
{
   public interface IPreguntaFrecuenteRepo
    {
        Task<List<PreguntaFrecuente>> GetPreguntasFrecuentes();
        PreguntasFrecuentesDto GetPreguntasFrecuentes(int id);
        Task NewPreguntasFrecuentes(PreguntasFrecuentesDto entityDto);
        Task DeletePreguntasFrecuentes(int id);
        Task<bool> ModifyPreguntasFrecuentes(PreguntasFrecuentesDto entityDto);

    }
}
