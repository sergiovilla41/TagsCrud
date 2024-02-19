using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public interface IRolConfiguracionGeneracionArchivosRepo
    {
        bool CanReadDataSet(Guid dataset, string email);
    }
}
