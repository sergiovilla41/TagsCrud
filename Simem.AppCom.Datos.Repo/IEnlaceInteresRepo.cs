using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public interface IEnlaceInteresRepo
    {
        Task<List<EnlaceInteres>> GetEnlaceInteres();
        EnlaceInteresDto GetEnlaceInteres(int id);
        Task NewEnlaceInteres(EnlaceInteresDto entityDto);
        Task DeleteEnlaceInteres(int id);
        Task<bool> ModifyEnlaceInteres(EnlaceInteresDto entityDto);
    }
}
