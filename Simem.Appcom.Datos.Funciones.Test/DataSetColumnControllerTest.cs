using Azure.Core;
using EnvironmentConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class DataSetColumnControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly DataSetColumnController dataSetColumnController = new();

        public DataSetColumnControllerTest()
        {
            Connection.ConfigureConnections();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public async Task GetDataSetColumn()
        {
            var result = await dataSetColumnController.HttpGetDataSetColumn("AE3F2310-154D-4391-B17C-081A83BC6E0F").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetDataSetColumnNotContent()
        {
            var result = await dataSetColumnController.HttpGetDataSetColumn("7892eed3-690f-4509-824a-1f9ac1516f08").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task  GetDataSetColumnException()
        {
            var request = await dataSetColumnController.HttpGetDataSetColumn("AE3F2310-154D-4391-B17C-081A83BC6E0K").ConfigureAwait(true);
            var resultadoStatusCode = (ObjectResult)request;
            Assert.AreEqual(400, resultadoStatusCode.StatusCode);
        }

        [TestMethod]
        public async Task GetEstandarizacionRegistro()
        {
            var result = await dataSetColumnController.HttpGetEstandarizacionRegistro("135C1099-3F6A-4978-938C-4DDD4C3571BF").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task HttpGetDataSetColumnVariable()
        {
            var result = await dataSetColumnController.HttpGetDataSetColumnVariable("6531E8FF-2CF8-4C1C-8119-773379A5A6C4").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task HttpGetDataSetColumnVariableNotContent()
        {
            var result = await dataSetColumnController.HttpGetDataSetColumnVariable("7892eed3-690f-4509-824a-1f9ac1516f08").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }
    }
}


