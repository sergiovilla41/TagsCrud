using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
namespace Simem.AppCom.Datos.Repo
{
    public interface IDatoRepo
    {
      
        public DatoDto GetData(Guid idRegistry);
      
        public Task<DatoDto> GetDataVariable(string datasetId);
        
        public Task<List<LastDataUpdatedDto>> GetLastUpdatedData();    
        List<DatoDto> GetListData(string categoryIdString, string typeViewIdString, string tagsIdString, string ordenar, string textFilter);

        public ConjuntoDatosPaginaDto GetDataSets(string categoryId, string tagsId, string typeViewId, string textoABuscar, string ordenarPor, Paginador paginador, bool esPrivado = false);

        bool isPrivateDataSet(Guid idDataSet);
    }
}