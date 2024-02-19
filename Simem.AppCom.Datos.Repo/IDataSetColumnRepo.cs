using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public interface IDataSetColumnRepo
    {
        public Task<List<DataSetColumnDto>> GetDataSetColumns(Guid dataId);
        public Task<List<EstandarizacionRegistrosFiltroDto>> GetEstandarizacionRegistro(Guid dataId);
    }
}
