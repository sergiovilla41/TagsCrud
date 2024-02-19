using EnvironmentConfig;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;

namespace Simem.Appcom.Datos.Repo.VariablesRepo.Test
{
    [TestClass]
    public class VariablesRepoTest
    {
        private readonly VariableRepo variablesRepo;

        public VariablesRepoTest()
        {
            Connection.ConfigureConnections();
            variablesRepo = new();
        }

        [TestMethod]
        public void GetVariables()
        {
            List<ConfiguracionVariableDto> variableDto = variablesRepo.GetVariables();
            Assert.IsNotNull(variableDto);
        }

        [TestMethod]
        public void GetVariablesFilteredByTitle()
        {
            InventarioVariablesResultDto variableDto = variablesRepo.GetVariablesFilteredByTitle("AGC programado");
            Assert.IsNotNull(variableDto);
        }
        [TestMethod]
        public void GetVariablesFilteredByTitleJustLetter()
        {
            InventarioVariablesResultDto variableDto = variablesRepo.GetVariablesFilteredByTitle("0056a308-04cf-4de1-89a9-2d15a1b2d6ae");
            Assert.IsNotNull(variableDto);
        }
        [TestMethod]
        public void GetVariablesFilteredByTitleAll()
        {
            InventarioVariablesResultDto variableDto = variablesRepo.GetVariablesFilteredByTitle("");
            Assert.IsNotNull(variableDto);
        }

        [TestMethod]
        public void GetVariableById()
        {
            List<ConfiguracionVariableDto> variableDto = variablesRepo.GetVariableById(Guid.Parse("4DEA7EBE-26F7-4D0C-A5FE-C6F2CCDDD722"));
            Assert.IsNotNull(variableDto);
        }

        [TestMethod]
        public void RemoveAccentsTest()
        {
            string variableDto = VariableRepo.RemoveAccents("Canción");
            Assert.IsTrue(variableDto=="Cancion");
        }
    }
}
