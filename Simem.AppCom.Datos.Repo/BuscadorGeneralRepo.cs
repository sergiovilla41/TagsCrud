using Mapeos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Base.Utils;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using SImem.AppCom.Datos.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Simem.AppCom.Datos.Repo
{
    public class BuscadorGeneralRepo: IBuscadorGeneralRepo
    {
        private readonly DbContextSimem _baseContext;
        public BuscadorGeneralRepo()
        {
            _baseContext ??= new DbContextSimem();
        }

        public BuscadorGeneralPrcDto BuscarDatos(string filtro, string tipoContenido, string tagsId)
        {
            BuscadorGeneralPrcDto result = new BuscadorGeneralPrcDto();
            result.totalFilas = 0;
            result.resultadoJson = "";
            result.Etiquetas = "";
            try
            {
                filtro = (filtro == null) ? "" : filtro;
                tipoContenido = (tipoContenido == null) ? "" : tipoContenido;

                var resultadoFiltro = _baseContext.BuscadorGeneralPrcResult.FromSqlInterpolated($" EXEC [simem].[prc_BuscadorGeneralSimem] {filtro},{tipoContenido},{tagsId}").ToList();
                
                if (resultadoFiltro.Count > 0 && resultadoFiltro[0].totalFilas > 0)
                {
                   var dto = MapeoDatos.Mapper.Map<BuscadorGeneralPrcDto>(resultadoFiltro[0]);
                   return dto;
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public BuscadorGeneralConjuntoDatosPrcDto BuscarDatosConjuntoDatos(string categoriaId, string tipoVistaId, string tagsId, string? varsId, string filtro, string orden, bool privado)
        {
            BuscadorGeneralConjuntoDatosPrcDto result = new BuscadorGeneralConjuntoDatosPrcDto();
            result.totalFilas = 0;
            result.resultadoJson = "";
            result.etiquetas = "";
            try
            {
                categoriaId = (categoriaId == null) ? "" : categoriaId;
                tipoVistaId = (tipoVistaId == null) ? "" : tipoVistaId;
                tagsId = (tagsId == null) ? "" : tagsId;
                varsId = (varsId == null) ? "" : varsId;
                filtro = (filtro == null) ? "" : filtro;
                var resultadoFiltro = _baseContext.BuscadorGeneralConjuntoDatosPrcResult.FromSqlInterpolated($" EXEC [simem].[prc_BuscadorGenerarConjuntoDatos] {categoriaId},{tipoVistaId},{tagsId},{varsId}, {filtro},{orden},{privado}").ToList();
               
                if (resultadoFiltro.Count > 0 && resultadoFiltro[0].totalFilas > 0) { 
                    var dto = MapeoDatos.Mapper.Map<BuscadorGeneralConjuntoDatosPrcDto>(resultadoFiltro[0]);
                    return dto;
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public string BuscarDatosConjuntoDatosPrivados(string email)
        {
            try
            {
                var usuario = _baseContext.Usuario.Where(x => x.Correo == email).FirstOrDefault();
                var connDb = new SqlConnection(KeyVaultManager.GetSecretValue(KeyVaultTypes.SimemConnection));
                SqlCommand comm = new SqlCommand($"[simem].[prcBuscadorGenerarConjuntoDatosPrivados] '{usuario?.IdUsuario}'", connDb)
                {
                    CommandTimeout = 180
                };
                comm.Connection.Open();

                var sqlReader = comm.ExecuteReader(CommandBehavior.SequentialAccess);
                StringBuilder content = new StringBuilder();
                while (sqlReader.Read())
                {
                    content.Append(sqlReader.GetString(0));
                }
                return content.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
