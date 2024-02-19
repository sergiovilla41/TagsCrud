using EnvironmentConfig;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simem.Appcom.Datos.Controller.Test
{
    [TestClass]
    public class MenuControllerTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly MenuController menuController = new();

        public MenuControllerTest()
        {
            Connection.ConfigureConnections();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public async Task GetMegaMenu()
        {
            try
            {
                var request = await menuController.HttpGetMegaMenu();
                Assert.IsNotNull(request);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
