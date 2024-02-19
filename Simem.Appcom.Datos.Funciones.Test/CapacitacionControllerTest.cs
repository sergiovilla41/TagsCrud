using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvironmentConfig;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class CapacitacionControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly CapacitacionController controller;

        public CapacitacionControllerTest()
        {
            Connection.ConfigureConnections();
            controller = new();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public async Task GetCapacitacion()
        {
            try
            {
                var request = await controller.GetCapacitacion();
                Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}