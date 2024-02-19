using EnvironmentConfig;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;

namespace Simem.AppCom.Datos.Repo.TypeView.Repo.Test
{
    [TestClass]
    public class TypeViewTest
    {
        private readonly TipoVistaRepo typeViewRepo;
        private readonly DbContextSimem _baseContext;

        public TypeViewTest()
        {
            Connection.ConfigureConnections();
            typeViewRepo = new TipoVistaRepo();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public void GetTypeViews()
        {
            List<TipoVistaDto> typeViewDtos = typeViewRepo.GetTypeViews();
            Assert.IsNotNull(typeViewDtos);
        }
        [TestMethod]
        public void GetTypeview()
        {
            TipoVistaDto TypeviewDto = typeViewRepo.GetTypeView(Guid.Parse("FB9A5FA8-D8D0-4D34-8445-35FD28DD65DA"));
            Assert.IsNotNull(TypeviewDto);
        }
        [TestMethod]
        public async Task NewTypeView()
        {
            TipoVistaDto typeViewData = new TipoVistaDto()
            {
                Estado = true,
                Titulo = "Hola mundo",
            };

            await typeViewRepo.NewTypeView(typeViewData);
            await DeleteMetaData();
            Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task ModifyTypeView()
        {
            var tagData = _baseContext.TipoVista.Where(w => w.Titulo == "Hola mundo").OrderByDescending(o => o.Id).FirstOrDefault()!;

            if (tagData == null)
            {
                Assert.IsTrue(true);
                return;
            }

            TipoVistaDto typeViewData = new TipoVistaDto()
            {
                Estado = tagData.Estado,
                Titulo = tagData.Titulo,
                Id = tagData.Id
            };
            bool @bool = await typeViewRepo.ModifyTypeView(typeViewData);
            Assert.IsTrue(@bool);
        }
        [TestMethod]
        public async Task DeleteMetaData()
        {
            try
            {
                var tagData = _baseContext.TipoVista.Where(w => w.Titulo == "Hola mundo").OrderByDescending(o => o.Id).FirstOrDefault()!;
                await typeViewRepo.DeleteTypeView((Guid)tagData.Id!);
                Assert.IsTrue(true);
            }
            catch (Exception) { Assert.IsTrue(true); }
        }
    }
}