using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IBaseRolConfiguracionGeneracionArchivos
    {
        bool CanReadDataSet(Guid dataset, string email);
    }
}
