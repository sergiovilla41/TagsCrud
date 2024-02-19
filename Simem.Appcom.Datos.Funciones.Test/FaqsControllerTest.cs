using Azure.Core;
using EnvironmentConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class FaqsControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly FaqsController faqsController = new();

        public FaqsControllerTest()
        {
             Connection.ConfigureConnections();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public async Task TestA1SavePreguntasFrecuentes()
        {
            PreguntasFrecuentesDto preguntasFrecuentesData = new()
            {
                Titulo = "¿Qué se puede encontrar en la página principal? (Prueba)",
                Descripcion = "Una API (Application Programming Interfaces) es una interfaz que permite entregar, a nivel de programación, un conjunto de funcionalidades para que sean utilizadas por los programas de otros usuarios. Por ejemplo, algunos servicios de pronóstico del clima entregan datos confiables, de fuentes de agencias internacionales especializadas, a través de este tipo de interfaces. Una API no es una interfaz gráfica de interacción con el usuario final, está definida con un identificador único (URL) que responde a una petición (endpoint) y permite que dos sistemas se intercomuniquen y compartan datos que viajan empaquetados bajo un formato de texto común.",
                Estado = false,
            };
            var request = await faqsController.HttpSavePreguntasFrecuentes(preguntasFrecuentesData).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA2SavePreguntasFrecuentesNoConten()
        {
            var request = await faqsController.HttpUpdatePreguntasFrecuentes(null!).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestA3UpdatePreguntasFrecuentes()
        {
            string? id = _baseContext.PreguntaFrecuente.Where(w => w.Titulo == "¿Qué se puede encontrar en la página principal? (Prueba)").Select(s => s.IdPreguntaFrecuente).FirstOrDefault().ToString();
            PreguntasFrecuentesDto preguntasFrecuentesData = new()
            {
                Id = int.Parse(id),
                Titulo = "¿Qué se puede encontrar en la página principal? (Prueba)",
                Descripcion = "Una API (Application Programming Interfaces) es una interfaz que permite entregar, a nivel de programación, un conjunto de funcionalidades para que sean utilizadas por los programas de otros usuarios. Por ejemplo, algunos servicios de pronóstico del clima entregan datos confiables, de fuentes de agencias internacionales especializadas, a través de este tipo de interfaces. Una API no es una interfaz gráfica de interacción con el usuario final, está definida con un identificador único (URL) que responde a una petición (endpoint) y permite que dos sistemas se intercomuniquen y compartan datos que viajan empaquetados bajo un formato de texto común.",
                Estado = false,
            };

            var request = await faqsController.HttpUpdatePreguntasFrecuentes(preguntasFrecuentesData).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));

        }

        [TestMethod]
        public async Task TestA4UpdatePreguntasFrecuentesNoConten()
        {
            var request = await faqsController.HttpUpdatePreguntasFrecuentes(null!).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestA5DeletePreguntasFrecuentes()
        {
            string? id = _baseContext.PreguntaFrecuente.Where(w => w.Titulo == "¿Qué se puede encontrar en la página principal? (Prueba)").Select(s => s.IdPreguntaFrecuente).FirstOrDefault().ToString();
            var result = await faqsController.DeleteFaqs(id).ConfigureAwait(true);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestA6GetPreguntasFrecuentesAsync()
        {
            var result = await faqsController.HttpGetPreguntasFrecuentes().ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA7GetPreguntasFrecuentesId()
        {
            var result = await faqsController.HttpGetPreguntasFrecuentesId("1").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA8GetPreguntasFrecuentesIdNoContent()
        {
            var result = await faqsController.HttpGetPreguntasFrecuentesId("99999999").ConfigureAwait(true);
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestA9GetPreguntasFrecuentesIdBadRequest()
        {
            var request = await faqsController.HttpGetPreguntasFrecuentesId("").ConfigureAwait(true);
            var resultadoStatusCode = (ObjectResult)request;
            Assert.AreEqual(400, resultadoStatusCode.StatusCode);
        }

        [TestMethod]
        public async Task TestA5DeletePreguntasFrecuentesBadRequest()
        {
           
            var request = await faqsController.DeleteFaqs("").ConfigureAwait(true);
            var resultadoStatusCode = (ObjectResult)request;
            Assert.AreEqual(400, resultadoStatusCode.StatusCode);
        }

    }
}
