using System;
﻿using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simem.AppCom.Datos.Dominio;
using SImem.AppCom.Datos.Dto;

namespace Simem.AppCom.Datos.Core
{
    public class Dato : IBaseData
    {
        private readonly DatoRepo repo;
        public Dato()
        {
            repo ??= new DatoRepo();
        }
        public List<DatoDto> GetLstData()
        {
            return repo.GetLstData();
        }
        public DatoDto GetData(Guid idRegistry)
        {
            return repo.GetData(idRegistry);
        }

        public async Task<List<ResumenConjuntoDatoDto>> GetAllDataSets(Paginador? paginador, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await repo.GetAllDataSets(paginador, fechaInicio, fechaFin);
        }

        public async Task<List<ResumenInventarioVariableDownloadDto>> GetAllVariables(Paginador? paginador, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await repo.GetAllVariables(paginador, fechaInicio, fechaFin);
        }


        public async Task<DatoDto> GetDataVariable(string Id)
        {
            return await repo.GetDataVariable(Id);
        }
 
        public async Task<List<LastDataUpdatedDto>> GetLastUpdatedData()
        {
            return await repo.GetLastUpdatedData();
        }
        public List<DatoDto> GetListDatas(string categoryId, string typeViewId,string tagsIdString,string ordenar,string texto)
        {
            return repo.GetListData(categoryId,typeViewId, tagsIdString,ordenar,texto);
        }

        public ConjuntoDatosPaginaDto GetDataSets(string categoryId, string tagsId, string typeViewId, string textoABuscar, string ordenarPor, Paginador paginador, bool esPrivado = false)
        {
            return repo.GetDataSets(categoryId, tagsId, typeViewId, textoABuscar, ordenarPor, paginador, esPrivado);
        }

        public void AddView(Guid IdConfiguracionGeneracionArchivos)
        {
            repo.AddView(IdConfiguracionGeneracionArchivos);
        }

        public void AddViewVariable(string id)
        {
            repo.AddViewVariable(id);
        }

        public void AddDownload(Guid IdConfiguracionGeneracionArchivos)
        {
            repo.AddDownload(IdConfiguracionGeneracionArchivos);
        }

        public void AddDownloadVariable(string id)
        {
            repo.AddDownloadVariable(id);
        }

        public bool isPrivateDataSet(Guid idDataSet)
        {
            return repo.isPrivateDataSet(idDataSet);
        }

    }
}