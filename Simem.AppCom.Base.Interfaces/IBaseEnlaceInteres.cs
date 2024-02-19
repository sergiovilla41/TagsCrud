using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dominio;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IBaseEnlaceInteres
    {
        public Task<List<EnlaceInteres>> GetEnlaceInteres();
        public EnlaceInteresDto GetEnlaceInteres(int id);
        public Task NewEnlaceInteres(EnlaceInteresDto entityDto);
        public Task DeleteEnlaceInteres(int id);
        public Task<bool> ModifyEnlaceInteres(EnlaceInteresDto entityDto);
    }
}
