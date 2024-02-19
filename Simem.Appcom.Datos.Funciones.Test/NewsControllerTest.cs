using EnvironmentConfig;
using Microsoft.AspNetCore.Mvc;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class NewsControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly NewsController newsController;

        public NewsControllerTest()
        {
            Connection.ConfigureConnections();
            _baseContext ??= new DbContextSimem();
            newsController = new();
        }

        [TestMethod]
        public async Task Test1Novedades()
        {
            var request =await newsController.HttpGetNovedades(null,"", "{'pageIndex':1,'pageSize':10}","").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Test2NovedadesConjuntoDatos()
        {
            var request = await newsController.HttpGetNovedades("DEA97AD9-6AE7-40A7-996F-44CF52F63BC1", "", "{'pageIndex':1,'pageSize':10}", "81FD6BCA-3EB7-4DDD-BC85-88647FE1C90D").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Test3NovedadesPaginador()
        {
           
            var request = await newsController.HttpGetNovedades("30B40BB4-04A5-45D9-8C7A-428E07F9048D", "asignaciones", "{'pageIndex':1,'pageSize':10}","").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Test4NovedadesPaginadoError()
        {
            var request = await newsController.HttpGetNovedades(null, "",null,null).ConfigureAwait(true);
            var result = (StatusCodeResult)request;
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public async Task Test5NovedadesCategories()
        {
            var request = await newsController.HttpGetNovedadesCategories().ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Test6NovedadesDetail()
        {
            
            var request = await newsController.HttpGetNovedadDetail("D0C51B78-23AC-4816-B2E4-DDC53C90AC9F").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Test7NovedadesDetailNotContent()
        {
            var request = await newsController.HttpGetNovedadDetail("FECD3669-F053-453C-97CC-8F353617284D").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Test8NovedadesDetailError()
        {
            var request = await newsController.HttpGetNovedadDetail("ASDF").ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task Test9ConjuntoDatos()
        {
            var request = await newsController.Http_GetConjuntoDatos("DEA97AD9-6AE7-40A7-996F-44CF52F63BC2", "", "{'pageIndex':1,'pageSize':10}").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestB10ConjuntoDeDatosConNovedades()
        {
            var request = await newsController.HttpGetConjuntoDeDatosConNovedades().ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult) || request.GetType()==typeof(NoContentResult));
        }

    }
}
