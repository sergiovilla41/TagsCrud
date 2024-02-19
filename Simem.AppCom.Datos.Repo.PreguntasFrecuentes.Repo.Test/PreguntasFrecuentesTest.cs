using EnvironmentConfig;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;
using Simem.AppCom.Datos.Dominio;

namespace Simem.AppCom.Datos.Repo.PreguntasFrecuentes.Repo.Test
{
    [TestClass]
    public class PreguntasFrecuentesTest
    {
        private readonly PreguntaFrecuenteRepo preguntasFrecuentesRepo;
        private readonly DbContextSimem _baseContext;
        public PreguntasFrecuentesTest()
        {
            Connection.ConfigureConnections();
            preguntasFrecuentesRepo = new PreguntaFrecuenteRepo();
            _baseContext ??= new DbContextSimem();
        }
        [TestMethod]
        public void GetPreguntasFrecuentes()
        {
            Task<List<PreguntaFrecuente>> preguntasFrecuentesDtos = preguntasFrecuentesRepo.GetPreguntasFrecuentes();
            Assert.IsNotNull(preguntasFrecuentesDtos);
        }

        [TestInitialize]
        public async Task NewPreguntasFrecuentes()
        {
            // Crear
            PreguntasFrecuentesDto preguntasFrecuentesData = new PreguntasFrecuentesDto()
            {
                Titulo = "Test Title",
                Descripcion = "Test Description",
                Estado = true
            };

            await preguntasFrecuentesRepo.NewPreguntasFrecuentes(preguntasFrecuentesData);

            var insertedRecord = _baseContext.PreguntaFrecuente
                .OrderByDescending(o => o.IdPreguntaFrecuente)
                .FirstOrDefault(o => o.Titulo == preguntasFrecuentesData.Titulo);

            Assert.IsNotNull(insertedRecord, "Failed to create the record.");

            // Eliminar
            await preguntasFrecuentesRepo.DeletePreguntasFrecuentes(insertedRecord.IdPreguntaFrecuente);

            var deletedRecord = _baseContext.PreguntaFrecuente.FirstOrDefault(o => o.IdPreguntaFrecuente == insertedRecord.IdPreguntaFrecuente);
            Assert.IsNull(deletedRecord, "The record was not successfully deleted from the database.");
        }

        [TestMethod]
        public async Task ModifyPreguntasFrecuentes()
        {
            // Crear
            PreguntasFrecuentesDto createData = new PreguntasFrecuentesDto()
            {
                Titulo = "Test Title",
                Descripcion = "Test Description",
                Estado = true
            };

            await preguntasFrecuentesRepo.NewPreguntasFrecuentes(createData);

            var insertedRecord = _baseContext.PreguntaFrecuente
                .OrderByDescending(o => o.IdPreguntaFrecuente)
                .FirstOrDefault(o => o.Titulo == createData.Titulo);

            Assert.IsNotNull(insertedRecord, "Failed to create the record.");

            // Modificar
            PreguntasFrecuentesDto modifyData = new PreguntasFrecuentesDto()
            {
                Id = insertedRecord.IdPreguntaFrecuente,
                Estado = true,
                Titulo = "Modified Test Title",
                Descripcion = "Modified Test Description"
            };

            bool updateSuccess = await preguntasFrecuentesRepo.ModifyPreguntasFrecuentes(modifyData);
            Assert.IsTrue(updateSuccess, "Failed to update the record.");

            var updatedRecord = _baseContext.PreguntaFrecuente.FirstOrDefault(o => o.IdPreguntaFrecuente == insertedRecord.IdPreguntaFrecuente);
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            _baseContext.Entry(updatedRecord).Reload();
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.

            Assert.IsNotNull(updatedRecord, "The record does not exist after update.");
            Assert.AreEqual(modifyData.Titulo, updatedRecord.Titulo, "Title did not update correctly.");
            Assert.AreEqual(modifyData.Descripcion, updatedRecord.Descripcion, "Description did not update correctly.");

            // Eliminar
            await preguntasFrecuentesRepo.DeletePreguntasFrecuentes(updatedRecord.IdPreguntaFrecuente);

            var deletedRecord = _baseContext.PreguntaFrecuente.FirstOrDefault(o => o.IdPreguntaFrecuente == updatedRecord.IdPreguntaFrecuente);
            Assert.IsNull(deletedRecord, "The record was not successfully deleted from the database after update.");
        }

        [TestMethod]
        public void GetPreguntasFrecuentesId()
        {
            PreguntasFrecuentesDto preguntasFrecuentesDto = preguntasFrecuentesRepo.GetPreguntasFrecuentes(3);
            Assert.IsNotNull(preguntasFrecuentesDto);
        }

        [TestMethod]
        public async Task DeletePreguntasFrecuentesId()
        {
            // Crear primero para tener algo que eliminar
            PreguntasFrecuentesDto createData = new PreguntasFrecuentesDto()
            {
                Titulo = "Test Title for Deletion",
                Descripcion = "Test Description",
                Estado = true
            };

            await preguntasFrecuentesRepo.NewPreguntasFrecuentes(createData);

            var insertedRecord = _baseContext.PreguntaFrecuente
                .OrderByDescending(o => o.IdPreguntaFrecuente)
                .FirstOrDefault(o => o.Titulo == createData.Titulo);

            Assert.IsNotNull(insertedRecord, "Failed to create the record for deletion.");

            // Procedemos a eliminar
            await preguntasFrecuentesRepo.DeletePreguntasFrecuentes(insertedRecord.IdPreguntaFrecuente);

            var deletedRecord = _baseContext.PreguntaFrecuente.FirstOrDefault(o => o.IdPreguntaFrecuente == insertedRecord.IdPreguntaFrecuente);
            Assert.IsNull(deletedRecord, "The record was not successfully deleted from the database.");
        }
    }
}