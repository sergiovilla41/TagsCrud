using System;
﻿using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Repo;

namespace Simem.AppCom.Base.Interfaces
{
    public interface IBaseData
    {
        public List<DatoDto> GetLstData();
        public DatoDto GetData(Guid idRegistry);
       
        public Task<DatoDto> GetDataVariable(string Id);
        
        public Task<List<LastDataUpdatedDto>> GetLastUpdatedData();
        List<DatoDto> GetListDatas(string categoryId, string typeViewId, string tagsIdString, string ordenar, string texto);
        public ConjuntoDatosPaginaDto GetDataSets(string categoryId, string tagsId, string typeViewId, string textoABuscar, string ordenarPor, Paginador paginador, bool esPrivado=false);
        public bool isPrivateDataSet(Guid idDataSet);
    }
}
