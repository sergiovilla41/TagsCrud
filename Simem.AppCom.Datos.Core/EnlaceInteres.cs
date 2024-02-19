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
    public class EnlaceInteres : IBaseEnlaceInteres
    {
        private readonly EnlaceInteresRepo repo;
        public EnlaceInteres()
        {
            repo ??= new EnlaceInteresRepo();
        }
       

        public async Task<List<EnlaceInteresDto>> GetEnlaceInteres(Paginador paginador)
        {
            return await repo.GetEnlaceInteres(paginador); 
        }

        public async Task<List<Dominio.EnlaceInteres>> GetEnlaceInteres()
        {
            return await repo.GetEnlaceInteres();
        }

        public async Task<int> GetEnlaceinteresCount()
        {
            return await repo.GetEnlaceInteresCount();
        }

        public EnlaceInteresDto GetEnlaceInteres(int id)
        {
            return repo.GetEnlaceInteres(id);
        }
        public Task NewEnlaceInteres(EnlaceInteresDto entityDto)
        {
            return repo.NewEnlaceInteres(entityDto);
        }
        public Task DeleteEnlaceInteres(int id) { return repo.DeleteEnlaceInteres(id); }
        public Task<bool> ModifyEnlaceInteres(EnlaceInteresDto entityDto) { return repo.ModifyEnlaceInteres(entityDto); }
    }
}
