using AutoMapper.Internal;
using EnvironmentConfig;
using Microsoft.AspNetCore.Mvc;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class FileGenerationControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly FileGenerationController fileGenerationController = new();

        public FileGenerationControllerTest()
        {
             Connection.ConfigureConnections();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public async Task AddDownload()
        {
            IdRequest idRequest = new IdRequest() { Id = Guid.Parse("A263BD01-2E5A-47BE-B7D2-3EDFF580687F").ToString()};
            var request = await fileGenerationController.AddDownload(idRequest).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task AddDownloadNull()
        {
            var request = await fileGenerationController.AddDownload(null!).ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task AddDownloadGuidBadRequest()
        {
            IdRequest idRequest = new IdRequest() { Id = null! };
            var request = await fileGenerationController.AddDownload(idRequest).ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task AddDownloadBadRequest()
        {
            IdRequest idRequest = new IdRequest() { Id = "ASDF"};
            var request = await fileGenerationController.AddDownload(idRequest).ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task AddDownloadVariable()
        {
            IdRequest idRequest = new IdRequest() { Id = Guid.Parse("6531E8FF-2CF8-4C1C-8119-773379A5A6C4").ToString()};
            var request = await fileGenerationController.AddDownloadVariable(idRequest).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task AddDownloadVariableNull()
        {
            var request = await fileGenerationController.AddDownloadVariable(null!).ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task AddViewCount()
        {
            IdRequest idRequest = new IdRequest() { Id = Guid.Parse("A263BD01-2E5A-47BE-B7D2-3EDFF580687F").ToString() };
            var request = await fileGenerationController.AddViewCount(idRequest).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task AddViewCountGuidBadRequest()
        {
            IdRequest idRequest = new IdRequest() { Id = null!};
            var request = await fileGenerationController.AddViewCount(idRequest).ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task AddViewCountBadRequest()
        {
            var request = await fileGenerationController.AddViewCount(null!).ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task AddViewVariableCount()
        {
            IdRequest idRequest = new IdRequest() { Id = Guid.Parse("6531E8FF-2CF8-4C1C-8119-773379A5A6C4").ToString() };
            var request = await fileGenerationController.AddViewVariableCount(idRequest).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task AddViewVariableCountNull()
        {
            var request = await fileGenerationController.AddViewVariableCount(null!).ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task GetData()
        {
            var result = await fileGenerationController.HttpGetData("A704EEF3-CA1C-4EBF-98A0-01325C61765D").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task HttpGetDataVariable()
        {
            var result = await fileGenerationController.HttpGetDataVariable("6531E8FF-2CF8-4C1C-8119-773379A5A6C4").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCatalogVariableDatasetInventario()
        {
            var result = await fileGenerationController.GetCatalogVariableDataset("6531E8FF-2CF8-4C1C-8119-773379A5A6C4", null, null).ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCatalogVariableDatasetCatalogo()
        {
            var result = await fileGenerationController.GetCatalogVariableDataset("51FC0A59-3A00-462C-B449-9CB8D5E007FB", null, null).ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task FilterInventario()
        {
            DateTime fechaInicio = new DateTime(2023, 7, 01);
            DateTime fechaFin = new DateTime(2023, 7, 01);
            var result = await fileGenerationController.Filter("6531E8FF-2CF8-4C1C-8119-773379A5A6C4", fechaInicio, fechaFin, null, null).ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult) || result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task FilterCatalogo()
        {
            DateTime fechaInicio = new DateTime(2023, 1, 01);
            DateTime fechaFin = new DateTime(2023, 12, 01);
            var result = await fileGenerationController.Filter("51FC0A59-3A00-462C-B449-9CB8D5E007FB", fechaInicio, fechaFin, null, null).ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetLastUpdatedDatas()
        {
            var request = await fileGenerationController.HttpGetLastUpdatedDatas("false").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task GetListData()
        {
            var result = await fileGenerationController.HttpGetListData("D74B2E87-D9F0-44F2-BB02-85F520217E98",
                                       "",
                                       "",
                                       "asc",
                                       "").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public void GetLstData()
        {
            var request = fileGenerationController.HttpGetLstData();
            Assert.IsNotNull(request);
        }
    }
}
