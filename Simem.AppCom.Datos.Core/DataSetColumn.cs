using Simem.AppCom.Base.Interfaces;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Core
{
    public class DataSetColumn: IBaseDataSetColumn
    {
        private readonly ColumnaConjuntoDatoRepo dataSetColumnRepo;
        public DataSetColumn()
        {
            dataSetColumnRepo ??= new ColumnaConjuntoDatoRepo();
        }

        public async Task<List<DataSetColumnDto>> GetDataSetColumns(Guid dataId)
        {
            return await dataSetColumnRepo.GetDataSetColumns(dataId);
        }

        public async Task<List<EstandarizacionRegistrosFiltroDto>> GetEstandarizacionRegistro(Guid dataId)
        {
            return await dataSetColumnRepo.GetEstandarizacionRegistro(dataId);
        }

        public async Task<List<DataSetColumnDto>> GetDataSetColumnsVariable(string Id)
        {
            return await dataSetColumnRepo.GetDataSetColumnsVariable(Id);
        }
    }
}
