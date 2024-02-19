using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IBaseDataSetColumn
    {
        public Task<List<DataSetColumnDto>> GetDataSetColumns(Guid dataId);
        public Task<List<EstandarizacionRegistrosFiltroDto>> GetEstandarizacionRegistro(Guid dataId);
        public Task<List<DataSetColumnDto>> GetDataSetColumnsVariable(string Id);
    }
}
