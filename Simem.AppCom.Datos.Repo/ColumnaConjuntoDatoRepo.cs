using Mapeos;
using Microsoft.EntityFrameworkCore;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public class ColumnaConjuntoDatoRepo : IDataSetColumnRepo
    {

        private readonly DbContextSimem _baseContext;
        public ColumnaConjuntoDatoRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public async Task<List<DataSetColumnDto>> GetDataSetColumns(Guid dataId)
        {
            List<DataSetColumnDto> result = new List<DataSetColumnDto>();
            await Task.Run(() =>
            {
                var dataResult = (from dataSetColumn in _baseContext.ColumnaConjuntoDato
                                  join dataSetColumnData in _baseContext.ColumnaDatoConjuntoDato
                                  on dataSetColumn.IdColumnaDestino equals dataSetColumnData.IdColumnaDestino
                                  join data in _baseContext.GeneracionArchivo
                                  on dataSetColumnData.IdConfiguracionGeneracionArchivos equals data.IdConfiguracionGeneracionArchivos
                                  where data.IdConfiguracionGeneracionArchivos == dataId
                                  select new
                                  {
                                      dataSetColumn.IdColumnaDestino,
                                      dataSetColumn.NombreColumnaDestino,
                                      dataSetColumn.Descripcion,
                                      dataSetColumn.TipoDato,
                                      dataSetColumnData.NombreColumnaOrigen,
                                      dataSetColumnData.NumeracionColumna
                                    

                                  }).OrderBy( dc=> dc.NumeracionColumna ).ToList();

                foreach (var item in dataResult)
                {
                    DataSetColumnDto columData = new DataSetColumnDto() { 
                        IdColumnaDestino = item.IdColumnaDestino,
                        Nombre = item.NombreColumnaDestino,
                        Descripcion = item.Descripcion,
                        NombreColumnaOrigen = item.NombreColumnaOrigen,
                        NumeracionColumna = item.NumeracionColumna,
                        
                        TipoDato = item.TipoDato
                    };
                    result.Add(columData);
                }
            });
            return result;
        }

        public async Task<List<EstandarizacionRegistrosFiltroDto>> GetEstandarizacionRegistro(Guid dataId)
        {
            List<EstandarizacionRegistrosFiltroDto> lstReturn = new List<EstandarizacionRegistrosFiltroDto>();
            try
            {
                var dataResult = await (from dataSetColumn in _baseContext.ColumnaConjuntoDato
                                        join dataSetColumnData in _baseContext.ColumnaDatoConjuntoDato
                                        on dataSetColumn.IdColumnaDestino equals dataSetColumnData.IdColumnaDestino
                                        join data in _baseContext.GeneracionArchivo
                                        on dataSetColumnData.IdConfiguracionGeneracionArchivos equals data.IdConfiguracionGeneracionArchivos
                                        join estandarizacionRegistro in _baseContext.EstandarizacionRegistros
                                        on dataSetColumnData.IdColumnaDestino equals estandarizacionRegistro.IdColumnaDestino
                                        where data.IdConfiguracionGeneracionArchivos == dataId
                                        select new
                                        {
                                            estandarizacionRegistro.IdColumnaDestino,
                                            estandarizacionRegistro.ValorObjetivo

                                        }).ToListAsync();
                foreach (var item in dataResult)
                {
                    EstandarizacionRegistrosFiltroDto estandarizacion = new EstandarizacionRegistrosFiltroDto()
                    {
                        IdColumnaDestino = item.IdColumnaDestino,
                        ValorObjetivo = item.ValorObjetivo
                    };
                    lstReturn.Add(estandarizacion);
                }
                return lstReturn;
            }
            catch (Exception)
            {
                return lstReturn;
            }
        }

        public async Task<List<DataSetColumnDto>> GetDataSetColumnsVariable(string Id)
        {
            List<DataSetColumnDto> result = new List<DataSetColumnDto>();
            await Task.Run(() =>
            {
                var dataResult = (from dataSetColumn in _baseContext.ResumenConjuntoDatoColumna
                                  join data in _baseContext.ResumenConjuntoDato
                                  on dataSetColumn.IdResumenConjuntoDato equals data.IdResumenConjuntoDato
                                  where data.IdResumenConjuntoDato.ToString().ToLower().Equals(Id.ToLower())
                                  select new
                                  {
                                      dataSetColumn.IdResumenConjuntoDatoColumna,
                                      dataSetColumn.Nombre,
                                      dataSetColumn.Descripcion,
                                      dataSetColumn.TipoDato,
                                      dataSetColumn.OrdenColumna
                                  }).OrderBy(x => x.OrdenColumna).ToList();

                foreach (var item in dataResult)
                {
                    DataSetColumnDto columData = new DataSetColumnDto()
                    {
                        IdColumnaDestino = item.IdResumenConjuntoDatoColumna,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        TipoDato = item.TipoDato,
                        NumeracionColumna = item.OrdenColumna
                    };
                    result.Add(columData);
                }
            });
            return result;
        }
    }
}
