using Azure;
using EnvironmentConfig;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Repo;

namespace Simem.AppCom.Datos.Repo.Contacto.Repo.Test
{
    [TestClass]
    public class ContactoRepoTest
    {
        private readonly ContactoRepo _repo;
        private readonly DbContextSimem _baseContext;

        public ContactoRepoTest()
        {
            Connection.ConfigureConnections();
            _repo ??= new();
            _baseContext ??= new DbContextSimem();
        }

        [TestMethod]
        public async Task InsertContactRequest()
        {
            ContactoDto dto = new ContactoDto()
            {
                Id = new Guid("0B34BE72-61B5-4EC7-B556-5E68C5912345"),
                Token = "eyJhbGciOiJIUzI1NiJ9.eyJVc2VyIjoiUHJ1ZWJhIiwiSUQiOiIxMjM0NTYifQ.dfg584JLlKxpDgRm0ZZ70H2pJBQ_pfdUIFsJNHIEmto",
                ConsecutivoCRM = "P2023051234",
                FechaInicio = new DateTime(2023, 8, 1),
                FechaFin = null,
                FechaCreacion = new DateTime(2023, 8, 1)
            };

            await _repo.InsertContactRequest(dto);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task DeleteContactRequest()
        {
            try
            {
                Dominio.Contacto contacto = _baseContext.Contacto.FirstOrDefault(x => x.ConsecutivoCRM == "P2023051234")!;
                await _repo.DeleteContactRequest(contacto.Id);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetEmailRequest()
        {
            try
            {
                string correo = "jeisson.quintero@mvm.com.co";
                var response = await _repo.GetEmailRequest(correo);
                Assert.IsNotNull(response);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ObtenerListadoEmpresas()
        {
            try
            {
                var response = await Task.Run(() => _repo.GetCompanyList());
                Assert.IsNotNull(response);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ObtenerListadoPaises()
        {
            try
            {
                var response = await Task.Run(() => _repo.GetCountryList());
                Assert.IsNotNull(response);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ObtenerListadoSolicitantes()
        {
            try
            {
                var response = await Task.Run(() => _repo.GetApplicantList());
                Assert.IsNotNull(response);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ObtenerListadoTiposDocumento()
        {
            try
            {
                var response = await Task.Run(() => _repo.GetDocumentTypesList());
                Assert.IsNotNull(response);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ObtenerListadoTiposSolicitud()
        {
            try
            {
                var response = await Task.Run(() => _repo.GetRequestTypesList());
                Assert.IsNotNull(response);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}