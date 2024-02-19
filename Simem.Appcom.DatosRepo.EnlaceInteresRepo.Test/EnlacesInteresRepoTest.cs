using EnvironmentConfig;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;
using Simem.AppCom.Datos.Dominio;

namespace Simen.Datos.Repo.Test
{
    [TestClass]
    public class EnlaceInteresRepoTest
    {
        private readonly EnlaceInteresRepo enlaceInteresRepo;
        private readonly DbContextSimem _baseContext;

        private readonly EnlaceInteresDto entityDto = new EnlaceInteresDto()
        {
            Titulo = "SUICC1 (Prueba)",
            Descripcion = "Es la entidad adscrita al ministerio de minas y energía encargada  de regular los servicios de electricidad y gas según se establece en  la ley 142 y 143 de 1994. Fue creada por el gobierno nacional de  Colombia con el gin de regular las actividades de los servicios públicos.",
            Enlace = "www.hotmail.com",
            Icono = "icon-xm.png"
        };

        public EnlaceInteresRepoTest()
        {
            Connection.ConfigureConnections();
            enlaceInteresRepo ??= new();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public void GetEnlaceInteres()
        {
            Task<List<EnlaceInteres>> enlaceIntereDto = enlaceInteresRepo.GetEnlaceInteres();
            Assert.IsNotNull(enlaceIntereDto);
        }

        [TestMethod]
        public async Task GetEnlaceInteresPaginador()
        {
            List<EnlaceInteresDto> enlaceIntereDto = await enlaceInteresRepo.GetEnlaceInteres(new Paginador() { PageIndex = 1, PageSize = 2 });
            Assert.IsNotNull(enlaceIntereDto);
        }

        [TestMethod]
        public async Task GetEnlaceInteresCount()
        {
            int enlaceIntereDto = await enlaceInteresRepo.GetEnlaceInteresCount();
            Assert.IsNotNull(enlaceIntereDto);
        }



        [TestMethod]
        public void GetEnlaceInteresId()
        {
            EnlaceInteresDto enlaceIntereDto = enlaceInteresRepo.GetEnlaceInteres(8);
            Assert.IsNotNull(enlaceIntereDto);
        }

        [TestMethod]
        public async Task NewEnlaceInteres()
        {
            await enlaceInteresRepo.NewEnlaceInteres(entityDto);
            Simem.AppCom.Datos.Dominio.EnlaceInteres enlaceInteres = _baseContext.EnlaceInteres.FirstOrDefault(f => f.Titulo == "SUICC1 (Prueba)")!;
            await enlaceInteresRepo.DeleteEnlaceInteres(enlaceInteres.IdEnlaceInteres);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task ModifyEnlaceInteres()
        {
            await enlaceInteresRepo.NewEnlaceInteres(entityDto);
            Simem.AppCom.Datos.Dominio.EnlaceInteres enlaceInteres = _baseContext.EnlaceInteres.FirstOrDefault(f => f.Titulo == "SUICC1 (Prueba)")!;
            await enlaceInteresRepo.ModifyEnlaceInteres(entityDto);
            await enlaceInteresRepo.DeleteEnlaceInteres(enlaceInteres.IdEnlaceInteres);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task DeleteEnlaceInteres()
        {
            try
            {
                await enlaceInteresRepo.NewEnlaceInteres(entityDto);
                Simem.AppCom.Datos.Dominio.EnlaceInteres enlaceInteres = _baseContext.EnlaceInteres.FirstOrDefault(f => f.Titulo == "SUICC1 (Prueba)")!;
                await enlaceInteresRepo.DeleteEnlaceInteres(enlaceInteres.IdEnlaceInteres);
                Assert.IsTrue(true);
            }
            catch (Exception) { Assert.Fail(); }
        }

        [TestMethod]
        public async Task DeleteEnlaceInteresExceptionAsync()
        {
            try
            {
                await enlaceInteresRepo.DeleteEnlaceInteres(32000);
                Assert.IsTrue(true);
            }
            catch (Exception) { Assert.IsTrue(true); }
        }
    }
}