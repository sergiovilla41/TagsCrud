using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Core
{
    public class PreguntaFrecuente : IBasePreguntasFrecuentes
    {
        private readonly PreguntaFrecuenteRepo repo;
        public PreguntaFrecuente()
        {
            repo ??= new PreguntaFrecuenteRepo();
        }
        public async Task<List<Dominio.PreguntaFrecuente>> GetPreguntasFrecuentes()
        {
            return await repo.GetPreguntasFrecuentes();
        }
        public PreguntasFrecuentesDto GetPreguntasFrecuentes(int id)
        {
            return repo.GetPreguntasFrecuentes(id);
        }
        public Task NewPreguntasFrecuentes(PreguntasFrecuentesDto entityDto)
        {
            return repo.NewPreguntasFrecuentes(entityDto);
        }
        public Task DeletePreguntasFrecuentes(int id) { return repo.DeletePreguntasFrecuentes(id); }
        public Task<bool> ModifyPreguntasFrecuentes(PreguntasFrecuentesDto entityDto) { return repo.ModifyPreguntasFrecuentes(entityDto); }
    }
}
