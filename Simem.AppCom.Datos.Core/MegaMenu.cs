using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Core
{
    public class MegaMenu : IBaseMegaMenu
    {
        private readonly MegaMenuRepo megaMenuRepo;

        public MegaMenu()
        {
            megaMenuRepo ??= new MegaMenuRepo();
        }
        public async Task<List<MegaMenuDto>> GetMegaMenuComplete()
        {
            return await megaMenuRepo.GetMegaMenuComplete();
        }


    }
}
