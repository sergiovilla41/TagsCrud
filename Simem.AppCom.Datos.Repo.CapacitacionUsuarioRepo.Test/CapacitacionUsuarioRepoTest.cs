using EnvironmentConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Core;
using Simem.AppCom.Datos.Dominio;
using Simem.AppCom.Datos.Dto;


namespace Simem.AppCom.Datos.Repo
{
    [TestClass]
    public class CapacitacionUsuarioTest
    {
        private readonly CapacitacionUsuarioRepo capacitacionUsuarioRepo;
       
        public CapacitacionUsuarioTest()
        {
            Connection.ConfigureConnections();
            capacitacionUsuarioRepo = new();
        }

        [TestMethod]
        public async Task GetCapacitaciones()
        {
            List<Capacitacion> capacitacionUsuarioDtos = await capacitacionUsuarioRepo.GetCapacitaciones().ConfigureAwait(true);
            Assert.IsNotNull(capacitacionUsuarioDtos);
        }
    }
}