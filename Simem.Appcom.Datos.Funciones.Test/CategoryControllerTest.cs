using EnvironmentConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
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
    public class CategoryControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly CategoryController categoryController;
        
        private readonly CategoriaDto categoria = new()
        {
            Id = Guid.Parse("9555999d-5f47-4ae6-9b17-4aa4254710b9"),
            Titulo = "Prueba nueva categoria",
            Icono = "SIN.svg",
            Estado = false,
            Descripcion = "Description",
            ConjuntoDato = 10,
            UltimaActualizacion = "2023-07-06",
            UltimaActualizacionDatoTitulo = "2023-07-06",
            UltimaActualizacionDatoId = Guid.Parse("D808D43D-E3DA-493D-AAB5-015A818ADE10"),
            Descarga = "10",
            privado = false,
            CantidadConjuntoDato = 10,
            OrdenCategoria = 130
        };

        
        public CategoryControllerTest()
        {
            Connection.ConfigureConnections();
            categoryController = new();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public async Task TestA1GetCategories()
        {
            var request = await  categoryController.HttpGetCategories().ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA2GetCategory()
        {
            var request = await categoryController.Http_GetCategory("34C7A757-6D13-43C0-9C38-0741E1807124").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA3GetCategoryNoContent()
        {
            var request = await categoryController.Http_GetCategory("E961B46B-A9BE-42F0-9A0F-ACD00B3E9E97").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestA4GetCategoryBadRequest()
        {
            var request = await categoryController.Http_GetCategory("").ConfigureAwait(true);
            var resultadoStatusCode = (ObjectResult)request;
            Assert.AreEqual(400, resultadoStatusCode.StatusCode);
        }

        [TestMethod]
        public async Task TestA5GetMigaPanCategoria()
        {
            var request = await categoryController.HttpGetMigaPanCategoria("D74B2E87-D9F0-44F2-BB02-85F520217E98").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(ContentResult));

        }

        [TestMethod]
        public async Task TestA6GetCategoriesHijosAsync()
        {
            var request = await categoryController.Http_GetCategoriesHijos().ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task TestA7GetCategoriesHijosPrivadoAsync()
        {
            var request = await new PrivateCategoryController().Http_GetCategoriesHijosPrivado().ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(ContentResult));
        }

        [TestMethod]
        public async Task TestA8SaveCategory()
        {
            var request = await categoryController.HttpSaveCategory(categoria).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestA9FNoContentSaveCategory()
        {
            
            var request = await categoryController.HttpSaveCategory(null!).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestB10UpdateCategory()
        {
            
            Categoria? categoriaUpdate = _baseContext.Categoria.FirstOrDefault(w => w.Titulo == "Prueba nueva categoria");
            categoria.Id = categoriaUpdate!.Id; 
            var request = await categoryController.HttpUpdateCategory(categoria).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestB11DeleteCategory()
        {
            string? id = _baseContext.Categoria.Where(w => w.Titulo == "Prueba nueva categoria").Select(s => s.Id).FirstOrDefault().ToString();
            var request = await categoryController.DeleteCategory(id!).ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TestB12FailDeleteCategory()
        {
            var request = await categoryController.DeleteCategory("").ConfigureAwait(true);
            var resultadoStatusCode = (ObjectResult)request;
            Assert.AreEqual(400, resultadoStatusCode.StatusCode);
        }

        [TestMethod]
        public async Task TestB13GetMigaPanCategoriaNoContent()
        {
            var request = await categoryController.HttpGetMigaPanCategoria("CC2F174A-4455-4515-8F29-50962C77FA21").ConfigureAwait(true);
            Assert.IsTrue(request.GetType() == typeof(NoContentResult));

        }
    }
}
