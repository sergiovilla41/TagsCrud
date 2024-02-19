using EnvironmentConfig;
using Simem.AppCom.Datos.Dto;

namespace Simem.AppCom.Datos.Repo.MegaMenu.Repo.Test
{
    [TestClass]
    public class GetMegaMenuTest
    {
        private readonly MegaMenuRepo megaMenuRepo;
        public GetMegaMenuTest() {
            Connection.ConfigureConnections();
            megaMenuRepo = new MegaMenuRepo();
        }
        [TestMethod]
        public async Task GetMegaMenuComplete()
        {
            List<MegaMenuDto> orderNum = await megaMenuRepo.GetMegaMenuComplete();
            Assert.IsNotNull(orderNum);
        }
    }
}