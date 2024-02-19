using EnvironmentConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simen.AppCom.Datos.Repo.AgentRepo.Test
{
    [TestClass]
    public class RolConfiguracionGeneracionArchivosRepoTest
    {
        private readonly RolConfiguracionGeneracionArchivosRepo RolConfigRepo;
        public RolConfiguracionGeneracionArchivosRepoTest()
        {
            Connection.ConfigureConnections();
            RolConfigRepo = new RolConfiguracionGeneracionArchivosRepo();
        }

        [TestMethod]
        public void CanReadDataSet()
        {
            Guid dataset = Guid.Parse("B0898698-218A-46FB-BA84-2A88A402C492");

            bool canRead = RolConfigRepo.CanReadDataSet(dataset, "16161@xm.com.co");

            Assert.IsTrue(true);
        }

    }
}
