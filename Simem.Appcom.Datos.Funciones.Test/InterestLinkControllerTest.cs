using EnvironmentConfig;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Core;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
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
    public class InterestLinkControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly InterestLinkController interestLinkController = new();

        private readonly EnlaceInteresDto enlaceInteres = new EnlaceInteresDto()
        {
            Titulo = "SUICC1 (Prueba)",
            Descripcion = "Es la entidad adscrita al ministerio de minas y energía encargada  de regular los servicios de electricidad y gas según se establece en  la ley 142 y 143 de 1994. Fue creada por el gobierno nacional de  Colombia con el gin de regular las actividades de los servicios públicos.",
            Enlace = "www.hotmail.com",
            Icono = "icon-xm.png"
        };


        public InterestLinkControllerTest()
        {
            Connection.ConfigureConnections();
            _baseContext ??= new DbContextSimem();
        }


        [TestMethod]
        public async Task TestA1GetPaginadorEnlaceInteres()
        {
            Paginador paginador = new()
            {
                PageIndex = 1,
                PageSize = 2
            };
            string enlaceInteresString = JsonConvert.SerializeObject(paginador);
            var request = await interestLinkController.HttpGetEnlaceInteres(enlaceInteresString).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public void TestA2GetNullPaginadorEnlaceInteres()
        {
            var request = interestLinkController.HttpGetEnlaceInteres("");
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public async Task TestA3GetEnlaceInteresId()
        {

            var result = await interestLinkController.HttpGetEnlaceInteresId("1").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA4SaveEnlaceInteres()
        {
            
            var request = await interestLinkController.HttpSaveEnlaceInteres(enlaceInteres).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }
    
        [TestMethod]
       public async Task TestA5FailSaveEnlaceInteres()
        {
            var request = await interestLinkController.HttpSaveEnlaceInteres(null!).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestA6UpdateEnlaceInteres()
        {

            var request = await interestLinkController.HttpUpdateEnlaceInteres(enlaceInteres).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }
    

        [TestMethod]
         public async Task TestA7FailUpdateEnlaceInteres()
        {
            var request = await interestLinkController.HttpUpdateEnlaceInteres(null!).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestA8DeleteEnlaceInteres()
        {
            string? id = _baseContext.EnlaceInteres.Where(w => w.Titulo == "SUICC1 (Prueba)").Select(s => s.IdEnlaceInteres).FirstOrDefault().ToString();
            var request = await interestLinkController.DeleteInterestLink(id).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestA9FailDeleteEnlaceInteres()
        {    
            var request = await interestLinkController.DeleteInterestLink("asdf").ConfigureAwait(true);
            var resultadoStatusCode = (ObjectResult)request;
            Assert.AreEqual(400, resultadoStatusCode.StatusCode);

        }
    }
}

