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
namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class TagsControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly TagsController tagsController = new();

        private readonly EnlaceDto tagsData = new EnlaceDto()
        {
            Estado = true,
            Titulo = "hola mundo test",
        };

        public TagsControllerTest()
        {
            Connection.ConfigureConnections();

            _baseContext ??= new DbContextSimem();
        }
        [TestMethod]
        public async Task TestA1GetTag()
        {
            Guid? id = _baseContext.Etiqueta.Select(s => s.Id).FirstOrDefault();            
            var request = await tagsController.HttpGetTag(id.ToString()!).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA2FailGetTag()
        {
            var request = await tagsController.HttpGetTag("").ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task TestA3GetTags()
        {
            var request = await tagsController.HttpGetTags().ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA4SaveTag()
        {
            var request = await tagsController.HttpSaveTag(tagsData).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA5FailSaveTag()
        {
            var request = await tagsController.HttpSaveTag(null!).ConfigureAwait(true);
            var result = (StatusCodeResult)request;
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public async Task TestA6UpdateTag()
        {
            var request = await tagsController.HttpUpdateTag(tagsData).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA7DeleteTag()
        {
            string? id = _baseContext.Etiqueta.Where(w => w.Titulo == "hola mundo test").Select(s => s.Id).FirstOrDefault().ToString();
            var request = await tagsController.DeleteTag(id!.ToString()).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestA8FailDeleteTag()
        {
            var request = await tagsController.DeleteTag("asdf").ConfigureAwait(true);
            var result = (ObjectResult)request;
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task TestA9GetTagNoContent()
        {
            var request = await tagsController.HttpGetTag("4b300f7d-8c61-4c41-9d76-22f727503e67").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }
    }
}
