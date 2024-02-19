using EnvironmentConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simem.AppCom.Datos.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataColumnTest
{
    [TestClass]
    public class DataSetColumnRepoTest
    {
        private readonly ColumnaConjuntoDatoRepo columnaConjuntoDatoRepo;
        public DataSetColumnRepoTest()
        {
            Connection.ConfigureConnections();
            columnaConjuntoDatoRepo = new ColumnaConjuntoDatoRepo();
        }
        [TestMethod]
        public void GetDataSetColumnsTest()
        {
            Guid dataId = new Guid("6439C70A-7B15-4041-B8F9-CAC84E419953");
            var data = columnaConjuntoDatoRepo.GetDataSetColumns(dataId);
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void GetEstandarizacionRegistro()
        {
            Guid dataId = new Guid("6439C70A-7B15-4041-B8F9-CAC84E419953");
            var data = columnaConjuntoDatoRepo.GetEstandarizacionRegistro(dataId);
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void GetDataSetColumnsVariable()
        {
            var data = columnaConjuntoDatoRepo.GetDataSetColumnsVariable("6531E8FF-2CF8-4C1C-8119-773379A5A6C4");
            Assert.IsNotNull(data);
        }
    }
}
