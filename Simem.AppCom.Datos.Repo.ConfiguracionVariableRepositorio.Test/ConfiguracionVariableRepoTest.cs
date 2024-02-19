using EnvironmentConfig;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;

namespace Simem.AppCom.Datos.Repo.ConfiguracionVariableRepositorio.Test
{
    [TestClass]
    public class ConfiguracionVariableRepoTest
    {
        private readonly ConfiguracionVariableRepo repository;

        public ConfiguracionVariableRepoTest()
        {
            Connection.ConfigureConnections();
            repository = new();
        }

        [TestMethod]
        public void GetVariables()
        {
            List<ConfiguracionVariableDto> mock = new();
            mock.Add(new ConfiguracionVariableDto
            {
                IdVariable = Guid.NewGuid(),
                CodVariable = "123",
                NombreVariable = "",
                UnidadMedida = "",
                Descripcion = "",
                Proceso = "",
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                FechaCreacion = DateTime.Now
            });

            var lista = repository.GetDataById(Guid.Parse("AE3F2310-154D-4391-B17C-081A83BC6E0F")).Result;

            Assert.IsTrue(lista.GetType() == mock.GetType());
        }
    }
}