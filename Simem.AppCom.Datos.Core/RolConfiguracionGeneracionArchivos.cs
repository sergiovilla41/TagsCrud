using Simem.AppCom.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Core
{
    [ExcludeFromCodeCoverage]
    public class RolConfiguracionGeneracionArchivos : IBaseRolConfiguracionGeneracionArchivos
    {
        private readonly Repo.RolConfiguracionGeneracionArchivosRepo configRepo;

        public RolConfiguracionGeneracionArchivos()
        {
            configRepo = new Repo.RolConfiguracionGeneracionArchivosRepo();
        }

        public bool CanReadDataSet(Guid dataset, string email)
        {
            return configRepo.CanReadDataSet(dataset, email);
        }
    }
}
