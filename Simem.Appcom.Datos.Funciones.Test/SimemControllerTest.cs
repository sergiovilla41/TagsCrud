using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvironmentConfig;
using System.Net.Http.Headers;
using System.Net;
using Simem.AppCom.Datos.Dto;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class SimemControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly SimemController controller;
        private Credencial objMpped;
      

        public SimemControllerTest()
        {
            Connection.ConfigureConnections();
            controller = new SimemController();
            _baseContext ??= new DbContextSimem();
            objMpped = new Credencial
            {
                nombre ="Llave Api",
                correo = "16161@XM.com.co",
            };
        }

        [TestMethod]
        public async Task ValidarDataset()
        {
            try
            {
                var result = await controller.HttpValidateUserAuth("A704EEF3-CA1C-4EBF-98A0-01325C61765D").ConfigureAwait(true);
                Assert.IsNotNull(result);
            }
            catch(Exception)
            {
                Assert.Fail();
            }
        }
    }
}
