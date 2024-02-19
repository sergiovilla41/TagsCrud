using Azure.Core;
using EnvironmentConfig;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Core;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Datos.Servicios.Controllers;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class SearchControllerTest
    {
        
        private readonly SearchController searchController;
        private readonly BuscadorGeneralRepo buscadorGeneralRepo;
        private BuscadorGeneral buscadorGeneralCore;

        private readonly DbContextSimem _baseContext;

        public SearchControllerTest()
        {
            Connection.ConfigureConnections();
            _baseContext = new DbContextSimem();
            buscadorGeneralRepo = new();
            searchController = new();
            buscadorGeneralCore = new();
        }
        [TestMethod]
        public async Task GetDatosFiltroGeneral()
        {
            try
            {
                var result = await searchController.HttpGetBuscadorGeneral("false", "generacion", "","").ConfigureAwait(true);
                Assert.IsTrue(result.GetType() == typeof(ContentResult));
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task GetDatosFiltroGeneralNoContent()
        {
            try
            {
                var result = await searchController.HttpGetBuscadorGeneral("false", "asdfad", "","").ConfigureAwait(true);
                Assert.IsTrue(result.GetType() == typeof(NoContentResult));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void FailGetDatosFiltroGeneral()
        {
            var result = searchController.HttpGetBuscadorGeneral("true", "generacion", "","").Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task HttpGetDataSets()
        {
            var result = await searchController.HttpGetDataSets("1F7AD6A8-5E49-4A8E-A0BC-25E3D04443B2", "", "","", "periodicidad","").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(ContentResult));
        }

        [TestMethod]
        public async Task HttpGetDataSetsNotcontent()
        {
            var result = await searchController.HttpGetDataSets("1F7AD6A8-5E49-4A8E-A0BC-25E3D04443B1", "", "","","periodicidad", "").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task GetDatosFiltroGeneralTipoContenido()
        {
            var result = await Task.Run(() => searchController.HttpGetBuscadorGeneral("true", "", "Datos",""));
            Assert.IsTrue(result.GetType() == typeof(ContentResult));
        }

        [TestMethod]
        public async Task HttpGetDataSetsPrivadoBadRequest()
        {
            var request = await searchController.HttpGetDataSetsPrivado().ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }



        [TestMethod]
        public void BuscarDatosConjuntoDatos()
        {
            AppCom.Datos.Core.BuscadorGeneral buscador = new AppCom.Datos.Core.BuscadorGeneral();
           
            var result = buscador.BuscarDatosConjuntoDatos("1F7AD6A8-5E49-4A8E-A0BC-25E3D04443B2", "", "", "", "", "periodicidad", false);
            Assert.IsTrue(result.totalFilas > 0);
        }

        [TestMethod]
        public void BuscarDatosConjuntoDatosPrivado()
        {
            GeneracionArchivo? generacionArchivo = _baseContext.GeneracionArchivo.FirstOrDefault(w => w.Privacidad);
            AppCom.Datos.Core.BuscadorGeneral buscador = new AppCom.Datos.Core.BuscadorGeneral();
            var result = buscador.BuscarDatosConjuntoDatos(generacionArchivo!.IdConfiguracionGeneracionArchivos.ToString(), "", "", "", "", "periodicidad", true);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BuscarDatosConjuntoDatosPrivadosRepo()
        {
            var result = buscadorGeneralRepo.BuscarDatosConjuntoDatosPrivados("lbastidas@xm.com.co");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BuscarDatosConjuntoDatosPrivadosCore()
        {
            var result = buscadorGeneralCore.BuscarDatosConjuntoDatosPrivados("lbastidas@xm.com.co");
            Assert.IsNotNull(result);
        }
    }
}