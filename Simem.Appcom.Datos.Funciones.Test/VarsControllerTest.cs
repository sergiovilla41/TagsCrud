using Microsoft.AspNetCore.Mvc;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvironmentConfig;
using Azure.Core;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class VarsControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly VarsController controller;

        public VarsControllerTest()
        {
            Connection.ConfigureConnections();
            controller = new VarsController();
            _baseContext ??= new DbContextSimem();
        }
        [TestMethod]
        public async Task GetVariableById()
        {          
            var result = await controller.HttpGetVariableById("76531DDE-4E94-4BEF-BEC7-00D3CD89123D").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetVariableByIdNotcontent()
        {
            var result = await controller.HttpGetVariableById("ce40fb2d-842a-4d9c-98f6-463661be9828").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task GetVariableFilteredByTitle()
        {
            var result = await controller.HttpGetVariableFilteredByTitle("Mediana de los datos").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }
        [TestMethod]
        public async Task GetVariableFilteredByTitleNoContent()
        {
            var result = await controller.HttpGetVariableFilteredByTitle("NoContent").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }
       
        [TestMethod]
        public async Task HttpGetDataConfiguracionVariable()
        {
            var result = await controller.HttpGetDataConfiguracionVariable("AE3F2310-154D-4391-B17C-081A83BC6E0F").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task HttpGetDataConfiguracionVariableNotContent()
        {
            var result = await controller.HttpGetDataConfiguracionVariable("911462f8-b237-4833-8d36-c0d6f8275333").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task HttpGetDataConfiguracionVariableBadRequest()
        {
            var request = await controller.HttpGetDataConfiguracionVariable("ASDF").ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task VariableInventory()
        {
            var result = await controller.HttpGetVariableInventory().ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }   
    }
}
