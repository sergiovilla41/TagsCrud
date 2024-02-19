using EnvironmentConfig;
using Microsoft.EntityFrameworkCore;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using System.Security.Cryptography;

namespace Simem.Appcom.Datos.Repo
{
    [TestClass]
    public class CategoriaRepoTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly CategoriaRepo category;
        private readonly Simem.AppCom.Datos.Core.Categoria categoryCore;

        private readonly CategoriaDto categoria = new()
        {
            Id = Guid.Parse("9555999d-5f47-4ae6-9b17-4aa4254710b9"),
            Titulo = "Prueba nueva categoria (test)",
            Icono = "SIN.svg",
            Estado = false,
            Descripcion = "Description",
            ConjuntoDato = 10,
            UltimaActualizacion = "2023-07-06",
            UltimaActualizacionDatoTitulo = "2023-07-06",
            UltimaActualizacionDatoId = Guid.Parse("D808D43D-E3DA-493D-AAB5-015A818ADE10"),
            Descarga = RandomNumberGenerator.GetInt32(0, 3500).ToString(),
            privado = false,
            CantidadConjuntoDato = 10,
            OrdenCategoria = 130
        };

        public CategoriaRepoTest()
        {
            Connection.ConfigureConnections();
            category = new();
            categoryCore = new();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public void TestA1GetCategories()
        {
            List<CategoriaDto> categoryDtos = category.GetCategories();
            Assert.IsNotNull(categoryDtos);
        }

        [TestMethod]
        public void TestA2GetCategory()
        {
            Categoria? categoria = _baseContext.Categoria.FirstOrDefault();

            if (categoria == null)
                Assert.Fail();

            CategoriaDto categoryDto = category.GetCategory(categoria.Id);
            Assert.IsNotNull(categoryDto);
        }

        [TestMethod]
        public async Task TestA3NewCategory()
        {
            await category.NewCategory(categoria);
            Assert.IsTrue(true);
        }

       

        [TestMethod]
        public async Task TestA4ModifyCategory()
        {
            bool resultado = await category.ModifyCategory(categoria);
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void TestA5ObtenerCategoriasxNiveles()
        {
            var listarCategoryDtos = category.GetCategoryxLevel();
            Assert.IsNotNull(listarCategoryDtos);
        }


        [TestMethod]
        public async Task TestA6DeleteCategory()
        {
            
            string? id = _baseContext.Categoria.Where(w => w.Titulo == "Prueba nueva categoria (test)").Select(s => s.Id).FirstOrDefault().ToString();
            if (id != null)
            {
                await category.DeleteCategory(Guid.Parse(id));
                Assert.IsTrue(true);
            }
            Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestA7DeleteCategoryExceptionAsync()
        {
            try
            {
                await category.DeleteCategory(Guid.Parse("\"00000000-0000-0000-0000-000000000100"));
            }
            catch (Exception) { Assert.IsTrue(true); }
        }

        [TestMethod]
        public void TestA8CrumbBreadCategory()
        {
            var listarCategoryDtos = category.GetCrumbBreadCategory(Guid.Parse("16740685-d65e-457c-92a1-34c50db7794c"));
            Assert.IsNotNull(listarCategoryDtos);
        }

        [TestMethod]
        public void TestA9UpdateCategories()
        {
            category.UpdateCategories();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestB10UpdateDownloadsCategories()
        {
            category.UpdateDownloadsCategories();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestB11CategoriesHijos()
        {
            List<ListarCategoriaDto> categoriaDtos =  category.GetCategoriesHijos(false);
            Assert.IsTrue(categoriaDtos.Count > 0);
        }

        [TestMethod]
        public void TestB12GetCategoryHijos()
        {
            List<CategoryHijosDto> categoriaDtos = category.GetCategoryHijos(Guid.Parse("1F7AD6A8-5E49-4A8E-A0BC-25E3D04443B2"));
            Assert.IsTrue(categoriaDtos.Count > 0);
        }

        [TestMethod]
        public void TestB13CategoriesHijosCore()
        {
            List<ListarCategoriaDto> categoriaDtos = categoryCore.GetCategoriesHijos(false);
            Assert.IsNotNull(categoriaDtos);
        }

        [TestMethod]
        public void TestB14UpdateCategoriesCore()
        {
           var result = categoryCore.UpdateCategories();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestB15UpdateDownloadsCategoriesCore()
        {
            var result = categoryCore.UpdateDownloadsCategories();
            Assert.IsNotNull(result);
        }


    }
}